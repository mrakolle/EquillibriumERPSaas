using EquillibriumERP.Core.Abstractions.Domain.Enums;

namespace EquillibriumERP.Core.Abstractions.Products;

public class ProductLookupResult
{
    public Guid Id { get; set; }

    public string ProductCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public ProductType ProductType { get; set; }

    public Guid ProductCategoryId { get; set; }

    public string? CasNumber { get; set; }

    public string Description { get; set; } = string.Empty;

    public decimal SellingPrice { get; set; }

    public decimal CostPrice { get; set; }

    public string UnitOfMeasure { get; set; } = string.Empty;

    public decimal TaxRate { get; set; }

    public bool IsActive { get; set; }
}