
namespace EquillibriumERP.Purchasing.Contracts;

public record SupplierDto(
    Guid Id,
    string SupplierCode,
    string Name,
    Guid? SupplierCategoryId,
    string? RegistrationNumber,
    string? VatNumber,
    string? TaxNumber,
    string? Email,
    string? Phone,
    string? Mobile,
    string? Website,
    int PaymentTerms,
    bool IsActive
);