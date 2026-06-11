namespace EquillibriumERP.Products.Application.DTOs;
public record UpdateProductDto(
    string Name,
    decimal SellingPrice,
    bool IsActive
);