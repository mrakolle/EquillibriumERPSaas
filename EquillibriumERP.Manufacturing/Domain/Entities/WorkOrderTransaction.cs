namespace EquillibriumERP.Manufacturing.Domain.Entities;
public class WorkOrderTransaction
{
    public Guid Id { get; set; }

    // 🔷 Core traceability
    public Guid WorkOrderId { get; set; }
    public Guid WorkOrderStepId { get; set; }

    // 🔷 Audit
    public DateTime ExecutedAt { get; set; }
    public Guid ExecutedByUserId { get; set; }

    public string? Workstation { get; set; }

    public Guid? WorkOrderMaterialId { get; set; }
    public string? RawMaterialLotNo { get; set; }

    // 🔷 Production values
    public decimal ExpectedQuantity { get; set; }
    public decimal ActualQuantity { get; set; }

    public decimal Variance => ActualQuantity - ExpectedQuantity;

    public string UnitOfMeasure { get; set; } = string.Empty;

    public string? Comment { get; set; }
}