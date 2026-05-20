using EquillibriumERP.Infrastructure.Persistence;
using EquillibriumERP.Sales.Application.Features.Products;
using EquillibriumERP.Sales.Application.Interfaces;
using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Sales.Infrastructure;

public class ProductService : IProductService
{
    private readonly TenantDbContext _db;

    public ProductService(TenantDbContext db)
    {
        _db = db;
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

        _db.Set<Product>().Add(product);
        await _db.SaveChangesAsync();

        return Map(product);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        return await _db.Set<Product>()
            .AsNoTracking()
            .Select(p => Map(p))
            .ToListAsync();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product = await _db.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == id);

        return product == null ? null : Map(product);
    }

    public async Task<ProductDto?> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        var product = await _db.Set<Product>().FindAsync(id);
        if (product == null) return null;

        product.Update(
            dto.ProductCode,
            dto.Name,
            dto.ProductType,
            dto.SellingPrice,
            dto.CostPrice,
            dto.ProductCategoryId,
            dto.Description
        );

        await _db.SaveChangesAsync();

        return Map(product);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _db.Set<Product>().FindAsync(id);
        if (product == null) return false;

        _db.Set<Product>().Remove(product);
        await _db.SaveChangesAsync();

        return true;
    }

    // ✔ SINGLE SOURCE OF TRUTH MAPPING
    private static ProductDto Map(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            ProductCode = product.ProductCode,
            Name = product.Name,
            Description = product.Description,
            ProductType = product.ProductType,
            SellingPrice = product.SellingPrice,
            CostPrice = product.CostPrice,
            ProductCategoryId = product.ProductCategoryId,
            IsActive = product.IsActive
        };
    }
}