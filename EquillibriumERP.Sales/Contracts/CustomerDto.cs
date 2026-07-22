using EquillibriumERP.Core.Abstractions.Domain.Enums;

namespace EquillibriumERP.Sales.Contracts;
public record CustomerDto(
    Guid Id,
    string CustomerCode,
    string Name,
    CustomerType CustomerType,
    string? Email,
    string? Phone,
    bool IsActive
);