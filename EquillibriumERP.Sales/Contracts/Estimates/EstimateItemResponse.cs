namespace EquillibriumERP.Sales.Contracts;

public record EstimateItemResponse(
    Guid Id,
    Guid ProductId,
    string ProductCode,
    string ProductName,
    string Description,
    string UnitOfMeasure,
    decimal Quantity,
    decimal UnitPrice,
    decimal DiscountPercent,
    decimal TaxRate,
    decimal LineSubtotal,
    decimal TaxAmount,
    decimal LineTotal
);