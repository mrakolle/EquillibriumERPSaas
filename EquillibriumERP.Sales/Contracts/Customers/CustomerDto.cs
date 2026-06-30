namespace EquillibriumERP.Sales.Contracts.Customers;
public record CustomerDto(
    Guid Id,
    string Name,
    string? Email
);