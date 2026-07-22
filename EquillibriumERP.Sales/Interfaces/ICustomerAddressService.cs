using EquillibriumERP.Sales.Contracts;

namespace EquillibriumERP.Sales.Interfaces;

public interface ICustomerAddressService
{
    Task<IReadOnlyList<CustomerAddressDto>> GetAllAsync(
        Guid customerId,
        CancellationToken ct = default);

    Task<CustomerAddressDto?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default);

    Task<CustomerAddressDto> CreateAsync(
        CreateCustomerAddressRequest request,
        CancellationToken ct = default);

    Task<CustomerAddressDto> UpdateAsync(
        Guid id,
        UpdateCustomerAddressRequest request,
        CancellationToken ct = default);

    Task DeleteAsync(
        Guid id,
        CancellationToken ct = default);
}