namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class BillOfMaterialItem
{
    public Guid Id { get; set; }

    public Guid BillOfMaterialId { get; set; }

    public Guid RawMaterialId { get; set; }

    public decimal Quantity { get; set; }

    public string? Unit { get; set; }

    public bool IsOptional { get; set; }

    public BillOfMaterial BillOfMaterial { get; set; } = null!;
}