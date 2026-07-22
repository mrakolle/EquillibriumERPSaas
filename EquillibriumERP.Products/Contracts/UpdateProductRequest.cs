using EquillibriumERP.Core.Abstractions.Domain.Enums;

namespace EquillibriumERP.Products.Contracts;
public record UpdateProductRequest(
    string Name,
    string ProductCode,
    ProductType ProductType,
    Guid ProductCategoryId,
    decimal SellingPrice,
    decimal CostPrice,
    string? CasNumber,
    string? Description,
    bool IsActive
);