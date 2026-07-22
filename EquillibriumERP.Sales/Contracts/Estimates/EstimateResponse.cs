using EquillibriumERP.Sales.Domain.Enums;

namespace EquillibriumERP.Sales.Contracts;

public record EstimateResponse(
    Guid Id,
    string QuoteNumber,
    string? Reference,

    Guid CustomerId,
    string CustomerCode,
    string CustomerName,
    string? CustomerEmail,
    string? CustomerPhone,
    string? CustomerVatNumber,

    EstimateStatus Status,

    DateTime EstimateDateUtc,
    DateTime? ExpiryDateUtc,

    string? Notes,

    decimal Subtotal,
    decimal DiscountAmount,
    decimal TaxAmount,
    decimal TotalAmount,

    IReadOnlyList<EstimateItemResponse> Items
);