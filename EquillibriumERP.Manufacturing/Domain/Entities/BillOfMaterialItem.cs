namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class BillOfMaterialItem
{
    public Guid Id { get; set; }

    public Guid BillOfMaterialId { get; set; }

    public Guid RawMaterialProductId { get; set; }

    public decimal Quantity { get; set; }

    public string UnitOfMeasure { get; set; } = string.Empty;

    public BillOfMaterial BillOfMaterial { get; set; } = null!;
}