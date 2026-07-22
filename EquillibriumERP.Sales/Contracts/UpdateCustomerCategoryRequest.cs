namespace EquillibriumERP.Sales.Contracts;

public record UpdateCustomerCategoryRequest(
    string Code,
    string Name,
    string? Description,
    int SortOrder,
    bool IsActive
);