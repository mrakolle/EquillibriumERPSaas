namespace EquillibriumERP.Manufacturing.Contracts;

public class CreateBomItemRequest
{
    public Guid RawMaterialProductId { get; set; }

    // 0.6000 = 60%
    public decimal Quantity { get; set; }

    public string UnitOfMeasure { get; set; } = string.Empty;
}