namespace EquillibriumERP.Sales.Contracts.Estimates;
public record CreateEstimateItemRequest(
    Guid ProductId,
    decimal Quantity
);