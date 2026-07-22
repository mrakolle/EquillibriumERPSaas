using EquillibriumERP.Core.Abstractions.Domain.Enums;

namespace EquillibriumERP.Products.Contracts;

public class CreateProductRequest
{
    public string Name { get; set; } = null!;

    public string ProductCode { get; set; } = null!;

    public ProductType ProductType { get; set; }

    public Guid ProductCategoryId { get; set; }

    public decimal SellingPrice { get; set; }

    public decimal CostPrice { get; set; }

    public string? CasNumber { get; set; }

    public string? Description { get; set; }

    public string UnitOfMeasure { get; set; } = "Each";

    public decimal TaxRate { get; set; }

    public bool IsActive { get; set; } = true;
}