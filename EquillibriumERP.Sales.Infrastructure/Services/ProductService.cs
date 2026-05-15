using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Infrastructure.Persistence;
using EquillibriumERP.Sales.Application.Features.Products;
using EquillibriumERP.Sales.Application.Interfaces;
using EquillibriumERP.Sales.Domain.Entities;

namespace EquillibriumERP.Sales.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly TenantDbContext _context;

    public ProductService(TenantDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product(
            dto.ProductCode,
            dto.Name,
            dto.ProductType,
            dto.SellingPrice,
            dto.CostPrice,
            dto.ProductCategoryId,
            dto.Description
        );

        _context.Set<Product>().Add(product);

        await _context.SaveChangesAsync();

        return await MapToDtoAsync(product.Id);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        return await _context.Set<Product>()
            .Include(p => p.Category)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ProductCode = p.ProductCode,
                ProductType = p.ProductType,
                SellingPrice = p.SellingPrice,
                CostPrice = p.CostPrice,
                ProductCategoryId = p.ProductCategoryId,
                ProductCategoryName = p.Category != null
                    ? p.Category.Name
                    : null
            })
            .ToListAsync();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Product>()
            .Include(p => p.Category)
            .Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ProductCode = p.ProductCode,
                ProductType = p.ProductType,
                SellingPrice = p.SellingPrice,
                CostPrice = p.CostPrice,
                ProductCategoryId = p.ProductCategoryId,
                ProductCategoryName = p.Category != null
                    ? p.Category.Name
                    : null
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ProductDto?> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        var product = await _context.Set<Product>()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return null;

        product.Update(
            dto.ProductCode,
            dto.Name,
            dto.ProductType,
            dto.SellingPrice,
            dto.CostPrice,
            dto.ProductCategoryId,
            dto.Description
        );

        await _context.SaveChangesAsync();

        return await MapToDtoAsync(product.Id);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _context.Set<Product>()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return false;

        _context.Set<Product>().Remove(product);

        await _context.SaveChangesAsync();

        return true;
    }

    private async Task<ProductDto> MapToDtoAsync(Guid id)
    {
        return await _context.Set<Product>()
            .Include(p => p.Category)
            .Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ProductCode = p.ProductCode,
                ProductType = p.ProductType,
                SellingPrice = p.SellingPrice,
                CostPrice = p.CostPrice,
                ProductCategoryId = p.ProductCategoryId,
                ProductCategoryName = p.Category != null
                    ? p.Category.Name
                    : null
            })
            .FirstAsync();
    }
}