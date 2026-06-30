namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class MaterialConsumption
{
    public Guid Id { get; set; }
    public Guid WorkOrderStepId { get; set; }
    public WorkOrderStep WorkOrderStep { get; set; } = null!;

    public Guid WorkOrderId { get; set; }

    public Guid RawMaterialProductId { get; set; }

    public string LotNumber { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public string UnitOfMeasure { get; set; } = string.Empty;

    public DateTime ConsumedAt { get; set; }

    public WorkOrder WorkOrder { get; set; } = null!;
}