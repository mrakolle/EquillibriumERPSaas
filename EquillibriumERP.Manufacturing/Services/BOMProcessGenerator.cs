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
public class BOMProcessGenerator
{
    /*private readonly ManufacturingDbContext _db;
    private readonly ITenantResolver _tenantResolver;
    private readonly ITenantContextualizer _tenantContextualizer;
    private readonly ITenantSession _tenantSession;
    private readonly IProductLookup _productLookup;

    public BOMProcessGenerator(
        ManufacturingDbContext db,
        ITenantResolver tenantResolver,
        ITenantContextualizer tenantContextualizer,
        ITenantSession tenantSession,
        IProductLookup productLookup)
    {
        _db = db;
        _tenantResolver = tenantResolver;
        _tenantContextualizer = tenantContextualizer;
        _tenantSession = tenantSession;
        _productLookup = productLookup;
    }

    public async Task<BOMProcess> GenerateAsync(
        Guid billOfMaterialId,
        CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var bom = await _db.BillOfMaterials
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == billOfMaterialId, ct)
            ?? throw new Exception("BOM not found");

        var process = new BOMProcess
        {
            Id = Guid.NewGuid(),
            BillOfMaterialId = bom.Id,
            Name = $"Process-{bom.Code}",
            Steps = new List<BOMProcessStep>()
        };

        var stepNumber = 1;

        foreach (var item in bom.Items)
        {
            var product = await _productLookup.GetByIdAsync(item.RawMaterialProductId, ct);

            process.Steps.Add(new BOMProcessStep
            {
                Id = Guid.NewGuid(),
                BOMProcessId = process.Id,
                StepNumber = stepNumber++,
                Action = $"Add {product?.Name ?? "Material"}",
                Status = StepStatus.Pending,
                Materials = new List<BOMProcessStepMaterial>
                {
                    new BOMProcessStepMaterial
                    {
                        Id = Guid.NewGuid(),
                        RawMaterialProductId = item.RawMaterialProductId,
                        Quantity = item.Quantity,
                        UnitOfMeasure = item.UnitOfMeasure
                    }
                }
            });
        }

        _db.BOMProcesses.Add(process);

        await _db.SaveChangesAsync(ct);

        return process;
    }*/
}