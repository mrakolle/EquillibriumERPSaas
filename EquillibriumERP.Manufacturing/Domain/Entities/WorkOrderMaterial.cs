namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class WorkOrderMaterial
{
    public Guid Id { get; set; }
    public Guid WorkOrderId { get; set; }
    public Guid RawMaterialProductId { get; set; }
    public decimal ExpectedQuantity { get; set; }
    public string UnitOfMeasure { get; set; } 
        = string.Empty;
    public decimal IssuedQuantity { get; set; }
    public decimal ConsumedQuantity { get; set; }
    public WorkOrder WorkOrder { get; set; } 
        = null!;
    
}