using EquillibriumERP.Sales.Domain.Enums;

namespace EquillibriumERP.Sales.Application.Features.Products;

public class ProductDto
{
    public Guid Id { get; set; }

    public string ProductCode { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public ProductType ProductType { get; set; }

    public decimal SellingPrice { get; set; }

    public decimal CostPrice { get; set; }

    public Guid? ProductCategoryId { get; set; }

    public bool IsActive { get; set; }
}