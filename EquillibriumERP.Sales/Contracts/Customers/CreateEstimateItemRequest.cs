
namespace EquillibriumERP.Sales.Contracts.Customers;
public record CreateEstimateItemRequest(
    Guid ProductId,
    decimal Quantity,
    decimal UnitPrice
);