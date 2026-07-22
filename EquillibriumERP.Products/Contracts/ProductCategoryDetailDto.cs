//using EquillibriumERP.Products.Domain.Enums,
namespace EquillibriumERP.Products.Contracts;
public record ProductCategoryDetailDto(
    Guid Id,
    string Name,
    int ProductType,
    string? Description,
    bool IsActive
);