using EquillibriumERP.Sales.Contracts;

namespace EquillibriumERP.Sales.Interfaces;

public interface ICustomerService
{
    Task<List<CustomerDto>> GetAllAsync();

    Task<CustomerDetailDto?> GetByIdAsync(Guid id);

    Task<CustomerDetailDto> CreateAsync(
        CreateCustomerRequest dto,
        CancellationToken ct = default);

    Task<CustomerDetailDto?> UpdateAsync(
        Guid id,
        UpdateCustomerRequest dto,
        CancellationToken ct = default);

    Task<bool> DeleteAsync(Guid id);
}