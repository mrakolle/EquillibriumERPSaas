using EquillibriumERP.Sales.Domain.Enums;

namespace EquillibriumERP.Sales.Contracts.Estimates;

public sealed record EstimateDto(
    Guid Id,
    Guid CustomerId,
    string QuoteNumber,
    DateTime EstimateDateUtc,
    EstimateStatus Status,
    DateTime? ExpiryDateUtc
);