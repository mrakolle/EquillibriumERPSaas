using EquillibriumERP.Sales.Domain.Enums;

namespace EquillibriumERP.Sales.Application.Features.Products;

public class CreateProductDto
{
    public string ProductCode { get; set; } = default!;

    public string Name { get; set; } = default!;

    public ProductType ProductType { get; set; }

    public decimal SellingPrice { get; set; }

    public decimal CostPrice { get; set; }

    public Guid? ProductCategoryId { get; set; }

    public string? Description { get; set; }
}