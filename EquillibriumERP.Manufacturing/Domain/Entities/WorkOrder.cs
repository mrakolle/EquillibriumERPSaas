using EquillibriumERP.Manufacturing.Domain.Enums;
using EquillibriumERP.Manufacturing.Domain.Entities;

namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class WorkOrder
{
    public Guid Id { get; set; }

    public Guid BillOfMaterialId { get; set; }

    public decimal PlannedQuantity { get; set; }

    public string UnitOfMeasure { get; set; } = string.Empty;

    public WorkOrderStatus Status { get; set; }
    public DateTime? CompletedAt { get; set; }

    public string? BatchNo { get; set; }

    public string? LotNo { get; set; }

    // NAVIGATION
    public BillOfMaterial BillOfMaterial { get; set; } = null!;

    public ICollection<WorkOrderMaterial> Materials { get; set; }
        = new List<WorkOrderMaterial>();

    public ICollection<WorkOrderStep> Steps { get; set; }
        = new List<WorkOrderStep>();

    public ICollection<MaterialConsumption> MaterialConsumptions { get; set; }
        = new List<MaterialConsumption>();

    public ICollection<ProductBatch> ProductBatches { get; set; }
        = new List<ProductBatch>();
}