using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquillibriumERP.Products.Contracts;

namespace EquillibriumERP.Products.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateAsync(CreateProductRequest dto, CancellationToken ct);

    Task<List<ProductDto>> GetAllAsync();

    Task<ProductDto?> GetByIdAsync(Guid id);

    Task<ProductDto?> UpdateAsync(
    Guid id,
    UpdateProductRequest dto,
    CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id);
}