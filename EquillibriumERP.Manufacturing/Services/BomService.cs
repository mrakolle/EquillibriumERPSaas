using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Manufacturing.Interfaces;
using EquillibriumERP.Manufacturing.Contracts;
using EquillibriumERP.Manufacturing.Domain.Enums;
using EquillibriumERP.Manufacturing.Domain.Entities;
using EquillibriumERP.Core.Abstractions.Products;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Manufacturing.Infrastructure.Persistence;

namespace EquillibriumERP.Manufacturing.Services;

public class BomService : IBomService
{
    private readonly ManufacturingDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    public BomService(
        ManufacturingDbContext db,
        ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
    }

    public async Task<Guid> CreateAsync(CreateBomRequest request, CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var bom = new BillOfMaterial
        {
            Id = Guid.NewGuid(),
            ProductId = request.ProductId,
            Code = request.Code,
            Name = request.Name,
            Description = request.Description,
            IsActive = true
        };

        // ITEMS (percentage stored in Quantity)
        foreach (var item in request.Items)
        {
            bom.Items.Add(new BillOfMaterialItem
            {
                Id = Guid.NewGuid(),
                BillOfMaterialId = bom.Id,
                RawMaterialProductId = item.RawMaterialProductId,
                Quantity = item.Quantity,
                UnitOfMeasure = item.UnitOfMeasure
            });
        }

        _db.BillOfMaterials.Add(bom);
        await _db.SaveChangesAsync(ct);

        return bom.Id;
    }
    public async Task<Guid> AddStepAsync(
    Guid bomId,
    CreateBOMStepRequest request,
    CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var bom = await _db.BillOfMaterials
            .FirstOrDefaultAsync(x => x.Id == bomId, ct);

        if (bom == null)
            throw new Exception("BOM not found");

        var step = new BOMStep
        {
            Id = Guid.NewGuid(),

            BillOfMaterialId = bomId,

            StepNumber = request.StepNumber,
            Description = request.Description,
            DurationMinutes = request.DurationMinutes,
            Type = request.Type,

            // 🔥 MATERIAL MAPPING (THIS IS THE FIX)
            RawMaterialProductId = request.RawMaterialProductId,
            QuantityPercentage = request.QuantityPercentage / 100m,

            Status = StepStatus.Pending
        };

        _db.BOMSteps.Add(step);

        await _db.SaveChangesAsync(ct);

        return step.Id;
    }
    public async Task<Guid> AddStepMaterialAsync(
    Guid stepId,
    CreateBOMStepMaterialRequest request,
    CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var step = await _db.BOMSteps
            .FirstOrDefaultAsync(x => x.Id == stepId, ct);

        if (step == null)
            throw new Exception("Step not found");

        await ValidateMaterialExistsInBomAsync(
            stepId,
            request.RawMaterialProductId,
            ct);
        await EnsureStepMaterialNotDuplicateAsync(
            stepId,
            request.RawMaterialProductId,
            ct);

        var material = new BOMStepMaterial
        {
            Id = Guid.NewGuid(),
            BOMStepId = step.Id,
            RawMaterialProductId = request.RawMaterialProductId
        };

        _db.BOMStepMaterials.Add(material);

        await _db.SaveChangesAsync(ct);

        return material.Id;
    }
    public async Task<List<BomDto>> GetAllAsync(CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        return await _db.BillOfMaterials
            .Include(x => x.Items)
            .Select(b => new BomDto
            {
                Id = b.Id,
                ProductId = b.ProductId,
                Code = b.Code,
                Name = b.Name,
                Description = b.Description,
                Items = b.Items.Select(i => new BomItemDto
                {
                    Id = i.Id,
                    RawMaterialProductId = i.RawMaterialProductId,
                    Quantity = i.Quantity,
                    UnitOfMeasure = i.UnitOfMeasure
                }).ToList()
            })
            .ToListAsync(ct);
    }
    public async Task<BomDto?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        return await _db.BillOfMaterials
            .Include(x => x.Items)
            .Where(b => b.Id == id)
            .Select(b => new BomDto
            {
                Id = b.Id,
                ProductId = b.ProductId,
                Code = b.Code,
                Name = b.Name,
                Description = b.Description,
                Items = b.Items.Select(i => new BomItemDto
                {
                    Id = i.Id,
                    RawMaterialProductId = i.RawMaterialProductId,
                    Quantity = i.Quantity,
                    UnitOfMeasure = i.UnitOfMeasure
                }).ToList()
            })
            .FirstOrDefaultAsync(ct);
    }
    public async Task<Guid> RecordConsumptionAsync(
    Guid stepId,
    RecordStepConsumptionRequest request,
    CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var stepExists = await _db.BOMSteps
            .AnyAsync(x => x.Id == stepId, ct);

        if (!stepExists)
            throw new Exception("BOM Step not found");

        var consumption = new StepMaterialConsumption
        {
            Id = Guid.NewGuid(),
            WorkOrderStepId = stepId,
            RawMaterialProductId = request.RawMaterialProductId,
            QuantityUsed = request.QuantityUsed,
            RecordedAt = DateTime.UtcNow
        };

        _db.StepMaterialConsumptions.Add(consumption);

        await _db.SaveChangesAsync(ct);

        return consumption.Id;
    }
    public async Task<List<StepExpectedMaterialDto>> GetStepExpectedAsync(
    Guid stepId,
    CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        return await _db.BOMStepMaterials
            .Where(x => x.BOMStepId == stepId)
            .Select(x => new StepExpectedMaterialDto
            {
                RawMaterialProductId = x.RawMaterialProductId,
                Quantity = x.Quantity
            })
            .ToListAsync(ct);
    }
    public async Task<StepVarianceDto> GetStepVarianceAsync(Guid stepId, CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var expected = await _db.BOMStepMaterials
            .Where(x => x.BOMStepId == stepId)
            .ToListAsync(ct);

        var actual = await _db.StepMaterialConsumptions
            .Where(x => x.WorkOrderStepId == stepId)
            .GroupBy(x => x.RawMaterialProductId)
            .Select(g => new
            {
                RawMaterialProductId = g.Key,
                Quantity = g.Sum(x => x.QuantityUsed)
            })
            .ToListAsync(ct);

        var result = expected.Select(e =>
        {
            var a = actual.FirstOrDefault(x => x.RawMaterialProductId == e.RawMaterialProductId);

            return new StepVarianceDto
            {
                RawMaterialProductId = e.RawMaterialProductId,
                Expected = e.Quantity,
                Actual = a?.Quantity ?? 0
            };
        }).ToList();

        // return first for now (we’ll improve later if needed)
        return result.First();
    }

    //----------------------------------------------------------
    //                    VALIDATIONS SECTION.                  
    //----------------------------------------------------------
    private async Task ValidateMaterialExistsInBomAsync(
    Guid stepId,
    Guid rawMaterialProductId,
    CancellationToken ct)
    {
        var bom = await _db.BillOfMaterials
            .Include(x => x.Items)
            .Include(x => x.Steps)
            .FirstOrDefaultAsync(x => x.Steps.Any(s => s.Id == stepId), ct);

        if (bom == null)
            throw new Exception("BOM not found for step");

        var exists = bom.Items.Any(i =>
            i.RawMaterialProductId == rawMaterialProductId);

        if (!exists)
            throw new Exception("Material not part of BOM items");
    }

    private async Task EnsureStepMaterialNotDuplicateAsync(
    Guid stepId,
    Guid rawMaterialProductId,
    CancellationToken ct)
    {
        var exists = await _db.BOMStepMaterials
            .AnyAsync(x =>
                x.BOMStepId == stepId &&
                x.RawMaterialProductId == rawMaterialProductId, ct);

        if (exists)
            throw new Exception("Material already exists in step");
    }
}
