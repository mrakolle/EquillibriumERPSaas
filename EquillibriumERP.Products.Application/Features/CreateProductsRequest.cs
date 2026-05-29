using EquillibriumERP.Products.Application.DTOs;

namespace EquillibriumERP.Products.Application.Features;

public sealed class CreateProductsRequest
{
    public CreateProductDto ProductDto { get; set; } = default!;

}