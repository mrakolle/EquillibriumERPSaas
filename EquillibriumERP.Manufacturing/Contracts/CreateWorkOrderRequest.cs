namespace EquillibriumERP.Manufacturing.Contracts;

public class CreateWorkOrderRequest
{
    public Guid BillOfMaterialId { get; set; }
    public decimal PlannedQuantity { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
}