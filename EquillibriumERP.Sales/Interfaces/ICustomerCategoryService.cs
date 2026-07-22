using EquillibriumERP.Sales.Contracts;

namespace EquillibriumERP.Sales.Interfaces;

public interface ICustomerCategoryService
{
    Task<List<CustomerCategoryDto>> GetAllAsync();

    Task<CustomerCategoryDetailDto?> GetByIdAsync(Guid id);

    Task<CustomerCategoryDetailDto> CreateAsync(
        CreateCustomerCategoryRequest request,
        CancellationToken ct = default);

    Task<CustomerCategoryDetailDto?> UpdateAsync(
        Guid id,
        UpdateCustomerCategoryRequest request,
        CancellationToken ct = default);

    Task<bool> DeleteAsync(Guid id);
}