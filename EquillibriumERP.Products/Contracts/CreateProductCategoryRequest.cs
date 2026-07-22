namespace EquillibriumERP.Products.Contracts;

public record CreateProductCategoryRequest(
    string Name,
    int ProductType,
    string? Description
);