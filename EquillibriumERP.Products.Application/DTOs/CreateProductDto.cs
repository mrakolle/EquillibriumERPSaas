namespace EquillibriumERP.Products.Application.DTOs;
public record CreateProductDto(
    string ProductCode,
    string Name,
    decimal SellingPrice
);