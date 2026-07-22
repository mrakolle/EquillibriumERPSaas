namespace EquillibriumERP.Sales.Contracts;

public record UpdateEstimateRequest(
    Guid CustomerId,
    string? Reference,
    DateTime? ExpiryDateUtc,
    string? Notes,
    IReadOnlyList<CreateEstimateItemRequest> Items
);