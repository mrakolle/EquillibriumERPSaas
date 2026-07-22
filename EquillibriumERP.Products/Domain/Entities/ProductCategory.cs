using EquillibriumERP.Core.Abstractions.Domain.Enums;

namespace EquillibriumERP.Products.Domain.Entities;

public class ProductCategory
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public ProductType ProductType { get; private set; }

    public string? Description { get; private set; }

    public bool IsActive { get; private set; } = true;

    public ICollection<Product> Products { get; private set; } = new List<Product>();

    private ProductCategory() { }

    public ProductCategory(
        string name,
        ProductType productType,
        string? description = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        ProductType = productType;
        Description = description;
        IsActive = true;
    }
    public void Update(
    string name,
    ProductType productType,
    string? description)
    {
        Name = name;
        ProductType = productType;
        Description = description;
    }
    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}