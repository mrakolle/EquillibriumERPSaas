using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Manufacturing.Interfaces;
using EquillibriumERP.Manufacturing.Contracts;
using EquillibriumERP.Manufacturing.Domain.Entities;
using EquillibriumERP.Core.Abstractions.Products;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Manufacturing.Infrastructure.Persistence;
using EquillibriumERP.Manufacturing.Domain.Enums;

namespace EquillibriumERP.Manufacturing.Services;
public class WorkOrderService : IWorkOrderService
{
     private readonly ManufacturingDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    public WorkOrderService(
        ManufacturingDbContext dbContext,
        ITenantContextualizer tenantContextualizer)
    {
        _db = dbContext;
        _tenantContextualizer = tenantContextualizer;
    }

    public async Task<Guid> CreateWorkOrderAsync(
    CreateWorkOrderRequest request,
    CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var billOfMaterial = await _db.BillOfMaterials
            .Include(x => x.Steps)
            .FirstOrDefaultAsync(x => x.Id == request.BillOfMaterialId, ct);

        if (billOfMaterial == null)
            throw new Exception("BillOfMaterial not found.");

        var workOrder = new WorkOrder
        {
            Id = Guid.NewGuid(),
            BillOfMaterialId = billOfMaterial.Id,
            PlannedQuantity = request.PlannedQuantity,
            UnitOfMeasure = request.UnitOfMeasure,
            Status = WorkOrderStatus.Draft,

            Materials = new List<WorkOrderMaterial>(),
            Steps = new List<WorkOrderStep>()
        };

        // -----------------------------------------
        // 1. Create WorkOrderMaterials (scaled)
        // -----------------------------------------
        var bomMaterials = await _db.BOMSteps
            .Where(x => x.BillOfMaterialId == billOfMaterial.Id)
            .ToListAsync(ct);

        foreach (var bomStep in bomMaterials)
        {
            if (bomStep.RawMaterialProductId == null)
                continue;

            var expectedQty = request.PlannedQuantity * bomStep.QuantityPercentage;

            var material = new WorkOrderMaterial
            {
                Id = Guid.NewGuid(),
                WorkOrderId = workOrder.Id,
                RawMaterialProductId = bomStep.RawMaterialProductId.Value,
                ExpectedQuantity = expectedQty,
                UnitOfMeasure = request.UnitOfMeasure
            };

            workOrder.Materials.Add(material);
        }

        // -----------------------------------------
        // 2. Create WorkOrderSteps (link + snapshot)
        // -----------------------------------------
        foreach (var bomStep in billOfMaterial.Steps.OrderBy(x => x.StepNumber))
        {
            var materialMatch = workOrder.Materials
                .FirstOrDefault(m => m.RawMaterialProductId == bomStep.RawMaterialProductId);

            workOrder.Steps.Add(new WorkOrderStep
            {
                Id = Guid.NewGuid(),
                WorkOrderId = workOrder.Id,
                BOMProcessStepId = bomStep.Id,

                StepNumber = bomStep.StepNumber,
                Action = bomStep.Description,

                Status = StepStatus.Pending,

                // 🔥 CLEAN LINK
                WorkOrderMaterialId = materialMatch?.Id
            });
        }

        _db.WorkOrders.Add(workOrder);
        await _db.SaveChangesAsync(ct);

        return workOrder.Id;
    }
    public async Task StartWorkOrderAsync(
    Guid workOrderId,
    CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var workOrder = await _db.WorkOrders
            .Include(x => x.Steps)
            .FirstOrDefaultAsync(x => x.Id == workOrderId, ct);

        if (workOrder == null)
            throw new Exception("WorkOrder not found.");

        if (workOrder.Status != WorkOrderStatus.Draft)
            throw new Exception("Only Draft WorkOrders can be started.");

        // 🔷 Move WorkOrder into execution state
        workOrder.Status = WorkOrderStatus.InProgress;

        // 🔷 Reset all steps to Pending (safe baseline)
        foreach (var step in workOrder.Steps)
        {
            step.Status = StepStatus.Pending;
            step.StartedAt = null;
            step.CompletedAt = null;
        }

        // 🔷 Activate FIRST step only
        var firstStep = workOrder.Steps
            .OrderBy(x => x.StepNumber)
            .FirstOrDefault();

        if (firstStep == null)
            throw new Exception("WorkOrder has no steps.");

        firstStep.Status = StepStatus.InProgress;
        firstStep.StartedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
    }

    public async Task StartStepAsync(
    Guid workOrderId,
    Guid workOrderStepId,
    CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var workOrder = await _db.WorkOrders
            .Include(x => x.Steps)
            .FirstOrDefaultAsync(x => x.Id == workOrderId, ct);

        if (workOrder == null)
            throw new Exception("WorkOrder not found.");

        if (workOrder.Status != WorkOrderStatus.InProgress)
            throw new Exception("WorkOrder is not in progress.");

        var step = workOrder.Steps
            .FirstOrDefault(x => x.Id == workOrderStepId);

        if (step == null)
            throw new Exception("WorkOrder step not found.");

        // 🔷 HARD RULE: only one active step allowed
        var activeStep = workOrder.Steps
            .FirstOrDefault(x => x.Status == StepStatus.InProgress);

        if (activeStep != null && activeStep.Id != step.Id)
            throw new Exception("Another step is already in progress.");

        if (step.Status != StepStatus.Pending)
            throw new Exception("Only pending steps can be started.");

        var previousStep = workOrder.Steps
            .Where(x => x.StepNumber < step.StepNumber)
            .OrderByDescending(x => x.StepNumber)
            .FirstOrDefault();

        if (previousStep != null &&
            previousStep.Status != StepStatus.Completed)
        {
            throw new Exception("Previous step has not been completed.");
        }

        step.Status = StepStatus.InProgress;
        step.StartedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
    }

    public async Task ExecuteStepAsync(
    Guid workOrderId,
    Guid workOrderStepId,
    ExecuteStepRequest request,
    CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var workOrder = await GetWorkOrderForExecutionAsync(workOrderId, ct);

        var step = GetExecutableStep(workOrder, workOrderStepId);

        var material = GetStepMaterial(workOrder, step);

        var transaction = CreateWorkOrderTransaction(
            workOrder,
            step,
            material,
            request);

        _db.WorkOrderTransactions.Add(transaction);

        var isLastStep = IsLastStep(workOrder, step);

        CompleteStep(step);

        if (isLastStep)
        {
            workOrder.Status = WorkOrderStatus.Completed;
            workOrder.CompletedAt = DateTime.UtcNow;

            workOrder.BatchNo = GenerateBatchNo();
            workOrder.LotNo = GenerateLotNo();
        }

        await _db.SaveChangesAsync(ct);
    }

    private async Task<WorkOrder> GetWorkOrderForExecutionAsync(
    Guid workOrderId,
    CancellationToken ct)
    {
        var workOrder = await _db.WorkOrders
            .Include(x => x.Steps)
            .Include(x => x.Materials)
            .FirstOrDefaultAsync(x => x.Id == workOrderId, ct);

        if (workOrder == null)
            throw new Exception("WorkOrder not found.");

        if (workOrder.Status != WorkOrderStatus.InProgress)
            throw new Exception("WorkOrder is not in progress.");

        return workOrder;
    }

    private static WorkOrderStep GetExecutableStep(
    WorkOrder workOrder,
    Guid workOrderStepId)
    {
        var step = workOrder.Steps
            .FirstOrDefault(x => x.Id == workOrderStepId);

        if (step == null)
            throw new Exception("WorkOrder step not found.");

        if (step.Status != StepStatus.InProgress)
            throw new Exception("Step is not in progress.");

        return step;
    }

    private static WorkOrderMaterial? GetStepMaterial(
    WorkOrder workOrder,
    WorkOrderStep step)
    {
        if (!step.WorkOrderMaterialId.HasValue)
            return null;

        var material = workOrder.Materials
            .FirstOrDefault(x => x.Id == step.WorkOrderMaterialId.Value);

        if (material == null)
            throw new Exception("WorkOrder material not found.");

        return material;
    }

    private static WorkOrderTransaction CreateWorkOrderTransaction(
    WorkOrder workOrder,
    WorkOrderStep step,
    WorkOrderMaterial? material,
    ExecuteStepRequest request)
    {
        return new WorkOrderTransaction
        {
            Id = Guid.NewGuid(),

            WorkOrderId = workOrder.Id,
            WorkOrderStepId = step.Id,
            WorkOrderMaterialId = step.WorkOrderMaterialId,
            RawMaterialLotNo = request.RawMaterialLotNo,

            ExecutedAt = DateTime.UtcNow,
            ExecutedByUserId = Guid.Parse("d2127df3-b918-45b6-a7bd-dee4daa3380c"),
            Workstation = null,

            ExpectedQuantity = material?.ExpectedQuantity ?? 0m,
            ActualQuantity = request.ActualQuantity,

            UnitOfMeasure = material?.UnitOfMeasure ?? workOrder.UnitOfMeasure,
            Comment = request.Comment
        };
    }

    private static void CompleteStep(WorkOrderStep step)
    {
        step.Status = StepStatus.Completed;
        step.CompletedAt = DateTime.UtcNow;
    }
    private static bool IsLastStep(WorkOrder workOrder, WorkOrderStep step)
    {
        return workOrder.Steps
            .All(x => x.Status == StepStatus.Completed || x.Id == step.Id);
    }
    private static string GenerateBatchNo()
    {
        return $"BATCH-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
    }

    private static string GenerateLotNo()
    {
        return $"LOT-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..6].ToUpper()}";
    }

}