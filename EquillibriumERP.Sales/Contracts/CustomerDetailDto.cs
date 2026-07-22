using EquillibriumERP.Core.Abstractions.Domain.Enums;

namespace EquillibriumERP.Sales.Contracts;

public record CustomerDetailDto(
    Guid Id,
    string CustomerCode,
    string Name,
    CustomerType CustomerType,
    Guid? CustomerCategoryId,
    string? RegistrationNumber,
    string? VatNumber,
    string? TaxNumber,
    string? Email,
    string? Phone,
    string? Mobile,
    string? Website,
    decimal CreditLimit,
    int PaymentTerms,
    bool IsActive
);