
namespace EquillibriumERP.Purchasing.Contracts;
public record SupplierDto(
    Guid Id,
    string Name,
    string SupplierCode,
    string? ContactPerson,
    string? Email,
    string? PhoneNumber,
    bool IsActive
    );