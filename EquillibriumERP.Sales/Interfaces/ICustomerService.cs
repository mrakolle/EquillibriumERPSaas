using EquillibriumERP.Sales.Contracts.Customers;

namespace EquillibriumERP.Sales.Interfaces;
public interface ICustomerService
{
    Task<CustomerDto> CreateAsync(
        CreateCustomerRequest request,
        CancellationToken ct);

    Task<CustomerDto?> GetByIdAsync(
        Guid id,
        CancellationToken ct);
}