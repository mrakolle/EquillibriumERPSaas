using EquillibriumERP.Products.Contracts;

namespace EquillibriumERP.Products.Interfaces;

public interface IProductCategoryService
{
    Task<List<ProductCategoryDetailDto>> GetAllAsync();

    Task<ProductCategoryDetailDto?> GetByIdAsync(Guid id);

    Task<ProductCategoryDetailDto> CreateAsync(
        CreateProductCategoryRequest dto,
        CancellationToken ct = default);

    Task<ProductCategoryDetailDto?> UpdateAsync(
        Guid id,
        UpdateProductCategoryRequest dto,
        CancellationToken ct = default);

    Task<bool> DeleteAsync(Guid id);
}