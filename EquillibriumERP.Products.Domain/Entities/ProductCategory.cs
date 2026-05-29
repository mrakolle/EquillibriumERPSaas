namespace EquillibriumERP.Products.Domain.Entities;

public class ProductCategory
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = string.Empty;

    public ICollection<Product> Products { get; private set; }
        = new List<Product>();

    private ProductCategory() { }

    public ProductCategory(
        string name,
        string? description = null)
    {
        Id = Guid.NewGuid();

        Name = name;

        Description = description ?? string.Empty;
    }
}