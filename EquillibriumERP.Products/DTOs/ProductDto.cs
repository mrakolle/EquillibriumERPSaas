namespace EquillibriumERP.Products.Application.DTOs;

public record ProductDto(
    Guid Id,
    string ProductCode,
    string ProductName,
    decimal SellingPrice,
    bool IsActive
);


