using EquillibriumERP.Sales.Contracts;

namespace EquillibriumERP.Sales.Interfaces;

public interface ICustomerContactService
{
    Task<IReadOnlyList<CustomerContactDto>> GetAllAsync(
        Guid customerId,
        CancellationToken ct = default);

    Task<CustomerContactDto?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default);

    Task<CustomerContactDto> CreateAsync(
        CreateCustomerContactRequest request,
        CancellationToken ct = default);

    Task<CustomerContactDto> UpdateAsync(
        Guid id,
        UpdateCustomerContactRequest request,
        CancellationToken ct = default);

    Task DeleteAsync(
        Guid id,
        CancellationToken ct = default);
}