namespace EquillibriumERP.Manufacturing.Contracts;
public class CreateBOMStepMaterialRequest
{
    public Guid RawMaterialProductId { get; set; }

    public decimal Quantity { get; set; }

    public string UnitOfMeasure { get; set; } = string.Empty;
}