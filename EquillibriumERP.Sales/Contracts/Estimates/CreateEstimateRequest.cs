namespace EquillibriumERP.Sales.Contracts.Estimates;

public record CreateEstimateRequest(
    Guid CustomerId,
    string ReferenceNumber,
    DateTime ExpiryDateUtc,
    string? Notes,
    List<CreateEstimateItemRequest> Items
);

