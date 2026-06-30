using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Manufacturing.Domain.Entities;
using EquillibriumERP.Manufacturing.Domain.Enums;
using Microsoft.AspNetCore.Routing;
using EquillibriumERP.Manufacturing.Interfaces;
using EquillibriumERP.Manufacturing.Contracts;
using EquillibriumERP.Core.Abstractions.Products;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Manufacturing.Infrastructure.Persistence;

namespace EquillibriumERP.Manufacturing.Services;

public class MaterialConsumptionService : IMaterialConsumptionService
{
    private readonly ManufacturingDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    public MaterialConsumptionService(
        ManufacturingDbContext db,
        ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
    }

    public async Task ConsumeAsync(MaterialConsumptionRequest request, CancellationToken ct)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        // 1. Load WorkOrder Step (CORE ENTITY)
       var step = await _db.WorkOrderSteps
            .Include(x => x.Materials)
            .FirstOrDefaultAsync(x => x.Id == request.WorkOrderStepId, ct)
            ?? throw new Exception("Work order step not found");

        // 2. Validate step state
        if (step.Status != StepStatus.InProgress)
            throw new Exception("Step must be InProgress");

        // 3. Validate material belongs to step
        var required = step.Materials
            .FirstOrDefault(x => x.RawMaterialProductId == request.RawMaterialProductId)
            ?? throw new Exception("Material not part of this step");

        // 4. Get consumed quantity for THIS STEP ONLY
        var consumedQty = await _db.MaterialConsumptions
            .Where(x =>
                x.WorkOrderStepId == step.Id &&
                x.RawMaterialProductId == request.RawMaterialProductId)
            .SumAsync(x => x.Quantity, ct);

        var newTotal = consumedQty + request.Quantity;

        // 5. Enforce step limit
        if (newTotal > required.Quantity)
            throw new Exception("Over-consumption not allowed for this step");

        // 6. Save consumption
        _db.MaterialConsumptions.Add(new MaterialConsumption
        {
            Id = Guid.NewGuid(),
            WorkOrderId = step.WorkOrderId,
            WorkOrderStepId = step.Id,
            RawMaterialProductId = request.RawMaterialProductId,
            LotNumber = request.LotNumber,
            Quantity = request.Quantity,
            UnitOfMeasure = request.UnitOfMeasure,
            ConsumedAt = DateTime.UtcNow
        });

        await _db.SaveChangesAsync(ct);
    }
}