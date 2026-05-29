using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EquillibriumERP.Products.Application.DTOs;

namespace EquillibriumERP.Products.Application.Interfaces;

public interface IProductService
{
    Task<ProductDto> CreateAsync(CreateProductDto dto);

    Task<List<ProductDto>> GetAllAsync();

    Task<ProductDto?> GetByIdAsync(Guid id);

    Task<ProductDto?> UpdateAsync(Guid id, UpdateProductDto dto);

    Task<bool> DeleteAsync(Guid id);
}