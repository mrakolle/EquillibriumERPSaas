namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class BillOfMaterial
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public ICollection<BillOfMaterialItem> Items { get; set; }
        = new List<BillOfMaterialItem>();

    public ICollection<BOMStep> Steps { get; set; }
        = new List<BOMStep>();
}