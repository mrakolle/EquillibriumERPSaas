using EquillibriumERP.Sales.Domain.Enums;

namespace EquillibriumERP.Sales.Domain.Entities;

public class Product
{

    public Guid Id { get; private set; }

    public string ProductCode { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = string.Empty;

    public Guid? ProductCategoryId { get; private set; }

    public ProductType ProductType { get; private set; }

    public decimal SellingPrice { get; private set; }

    public decimal CostPrice { get; private set; }

    public bool IsActive { get; private set; }

    public ProductCategory? Category { get; private set; }

    private Product() { }

    public Product(
        string productCode,
        string name,
        ProductType productType,
        decimal sellingPrice,
        decimal costPrice,
        Guid? productCategoryId = null,
        string? description = null)
    {
        Id = Guid.NewGuid();

        ProductCode = productCode;

        Name = name;

        ProductType = productType;

        SellingPrice = sellingPrice;

        CostPrice = costPrice;

        ProductCategoryId = productCategoryId;

        Description = description ?? string.Empty;

        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
    public void Update(
    string productCode,
    string name,
    ProductType productType,
    decimal sellingPrice,
    decimal costPrice,
    Guid? productCategoryId,
    string? description)
    {
        ProductCode = productCode;
        Name = name;
        ProductType = productType;
        SellingPrice = sellingPrice;
        CostPrice = costPrice;
        ProductCategoryId = productCategoryId;
        Description = description ?? string.Empty;
    }
}