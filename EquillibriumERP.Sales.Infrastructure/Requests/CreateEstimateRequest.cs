namespace EquillibriumERP.Sales.Infrastructure.Requests;

public record CreateEstimateRequest(
    Guid CustomerId,
    DateTime ExpiryDateUtc,
    string? Notes,
    List<CreateEstimateItemRequest> Items
);

public record CreateEstimateItemRequest(
    Guid ProductId,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal TaxRate
);