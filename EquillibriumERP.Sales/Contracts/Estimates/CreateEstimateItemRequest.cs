namespace EquillibriumERP.Sales.Contracts;

public record CreateEstimateItemRequest(
    Guid ProductId,
    decimal Quantity,
    decimal UnitPrice,
    decimal DiscountPercent,
    decimal TaxRate
);