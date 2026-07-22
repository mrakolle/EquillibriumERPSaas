namespace EquillibriumERP.Manufacturing.Contracts;
public class BomItemDto
{
    public Guid Id { get; set; }

    public Guid RawMaterialProductId { get; set; }

    public string RawMaterialName { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public string UnitOfMeasure { get; set; } = string.Empty;
}