namespace EquillibriumERP.Sales.Application.Contracts.Estimates;
public record UpdateEstimateRequest(
    Guid CustomerId,
    DateTime ExpiryDateUtc,
    string? Notes,
    List<UpdateEstimateItemRequest> Items
);

public record UpdateEstimateItemRequest(
    Guid ProductId,
    string Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal TaxRate
);