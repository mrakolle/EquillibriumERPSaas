using EquillibriumERP.Products.Domain.Enums;

namespace EquillibriumERP.Products.Contracts;

public record ProductDto(
    Guid Id,
    string ProductCode,
    string Name,
    ProductType ProductType,
    decimal SellingPrice,
    bool IsActive
);


