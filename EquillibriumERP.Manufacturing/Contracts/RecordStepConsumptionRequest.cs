namespace EquillibriumERP.Manufacturing.Contracts;
public class RecordStepConsumptionRequest
{
    public Guid RawMaterialProductId { get; set; }
    public decimal QuantityUsed { get; set; }
}