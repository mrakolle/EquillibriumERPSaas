using EquillibriumERP.Sales.Domain.Enums;

namespace EquillibriumERP.Sales.Contracts;

public record EstimateSummaryResponse(
    Guid Id,
    string ReferenceNumber,
    string CustomerName,
    EstimateStatus Status,
    DateTime EstimateDateUtc,
    decimal TotalAmount
);