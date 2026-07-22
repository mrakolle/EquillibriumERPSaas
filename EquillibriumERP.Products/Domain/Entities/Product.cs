using EquillibriumERP.Core.Abstractions.Domain.Enums;

namespace EquillibriumERP.Products.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }

    public string ProductCode { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string Description { get; private set; } = string.Empty;

    public Guid ProductCategoryId { get; private set; }

    public ProductCategory Category { get; private set; } = null!;
    public string UnitOfMeasure { get; private set; } = string.Empty;

    public decimal TaxRate { get; private set; }

    public string? CasNumber { get; private set; }

    public ProductType ProductType { get; private set; }

    public decimal SellingPrice { get; private set; }

    public decimal CostPrice { get; private set; }

    public bool IsActive { get; private set; } = true;

    private Product() { }

    public Product(
        string productCode,
        string name,
        ProductType productType,
        decimal sellingPrice,
        decimal costPrice,
        Guid productCategoryId,
        string? casNumber = null,
        string? description = null)
    {
        Id = Guid.NewGuid();

        ProductCode = productCode;
        Name = name;
        ProductType = productType;
        SellingPrice = sellingPrice;
        CostPrice = costPrice;

        ProductCategoryId = productCategoryId;

        CasNumber = casNumber;
        Description = description ?? string.Empty;

        IsActive = true;
    }
    public void Update(
    string name,
    string productCode,
    ProductType productType,
    Guid productCategoryId,
    decimal sellingPrice,
    decimal costPrice,
    string? casNumber,
    string? description)
{
    Name = name;
    ProductCode = productCode;
    ProductType = productType;
    ProductCategoryId = productCategoryId;
    SellingPrice = sellingPrice;
    CostPrice = costPrice;
    CasNumber = casNumber;
    Description = description ?? string.Empty;
}

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}