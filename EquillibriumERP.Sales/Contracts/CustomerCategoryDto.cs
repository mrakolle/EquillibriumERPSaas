namespace EquillibriumERP.Sales.Contracts;

public record CustomerCategoryDto(
    Guid Id,
    string Code,
    string Name,
    bool IsActive
);