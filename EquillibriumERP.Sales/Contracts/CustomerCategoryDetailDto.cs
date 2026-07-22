namespace EquillibriumERP.Sales.Contracts;

public record CustomerCategoryDetailDto(
    Guid Id,
    string Code,
    string Name,
    string? Description,
    int SortOrder,
    bool IsActive
);