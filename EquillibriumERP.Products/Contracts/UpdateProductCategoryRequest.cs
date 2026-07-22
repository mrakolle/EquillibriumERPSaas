namespace EquillibriumERP.Products.Contracts;

public record UpdateProductCategoryRequest(
    string Name,
    int ProductType,
    string? Description,
    bool IsActive
);