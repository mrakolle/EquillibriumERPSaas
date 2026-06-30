namespace EquillibriumERP.Manufacturing.Contracts;

public class MaterialConsumptionRequest
{
    public Guid WorkOrderStepId { get; set; }

    public Guid RawMaterialProductId { get; set; }

    public string LotNumber { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public string UnitOfMeasure { get; set; } = string.Empty;
}