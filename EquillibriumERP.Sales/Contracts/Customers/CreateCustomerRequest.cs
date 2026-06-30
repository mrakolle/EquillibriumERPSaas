namespace EquillibriumERP.Sales.Contracts.Customers;

public record CreateCustomerRequest(
    string Name,
    string? Email
);