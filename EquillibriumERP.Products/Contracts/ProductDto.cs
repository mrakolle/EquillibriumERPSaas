using EquillibriumERP.Core.Abstractions.Domain.Enums;

namespace EquillibriumERP.Products.Contracts;

public record ProductDto(
    Guid Id,
    string ProductCode,
    string Name,
    string? CasNumber,
    string Description,
    ProductType ProductType,
    decimal SellingPrice,
    bool IsActive
);

