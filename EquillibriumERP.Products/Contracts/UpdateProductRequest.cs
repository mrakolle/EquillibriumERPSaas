namespace EquillibriumERP.Products.Contracts;
public record UpdateProductRequest(
    string Name,
    decimal SellingPrice,
    bool IsActive
);