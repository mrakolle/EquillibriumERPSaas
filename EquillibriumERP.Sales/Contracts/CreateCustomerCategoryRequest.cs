namespace EquillibriumERP.Sales.Contracts;

public record CreateCustomerCategoryRequest(
    string Code,
    string Name,
    string? Description,
    int SortOrder
);