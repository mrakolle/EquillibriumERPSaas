namespace EquillibriumERP.Sales.Contracts;

public record CreateEstimateRequest(
    Guid CustomerId,
    string? Reference,
    DateTime? ExpiryDateUtc,
    string? Notes,
    IReadOnlyList<CreateEstimateItemRequest> Items
);