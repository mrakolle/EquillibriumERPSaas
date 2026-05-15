using EquillibriumERP.Sales.Application.Features.Products;

namespace EquillibriumERP.Sales.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateAsync(CreateProductDto dto);

    Task<List<ProductDto>> GetAllAsync();

    Task<ProductDto?> GetByIdAsync(Guid id);

    Task<ProductDto?> UpdateAsync(Guid id, UpdateProductDto dto);

    Task<bool> DeleteAsync(Guid id);
}