namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class BillOfMaterial
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<BillOfMaterialItem> Items { get; set; }
        = new List<BillOfMaterialItem>();
}