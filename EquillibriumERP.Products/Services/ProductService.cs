using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Products.Application.DTOs;
using EquillibriumERP.Products.Application.Interfaces;
using EquillibriumERP.Products.Domain.Entities;
using EquillibriumERP.Products.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Products.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly ITenantDbContext _db;
    private readonly ITenantResolver _tenantResolver;

    public ProductService(
        ITenantDbContext db,
        ITenantResolver tenantResolver)
    {
        _db = db;
        _tenantResolver = tenantResolver;
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        EnsureTenantContext();

        var product = new Product(
            dto.ProductCode,
            dto.Name,
            ProductType.FinishedGoods,
            dto.SellingPrice,
            0,
            null,
            null
        );

        _db.Set<Product>().Add(product);

        await _db.SaveChangesAsync();

        return Map(product);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        EnsureTenantContext();

        return await _db.Set<Product>()
            .AsNoTracking()
            .Select(p => Map(p))
            .ToListAsync();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        EnsureTenantContext();

        var product = await _db.Set<Product>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return product is null
            ? null
            : Map(product);
    }

    public async Task<ProductDto?> UpdateAsync(
        Guid id,
        UpdateProductDto dto)
    {
        EnsureTenantContext();

        var product = await _db.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product is null)
            return null;

        product.Update(
            product.ProductCode,
            dto.Name,
            product.ProductType,
            dto.SellingPrice,
            product.CostPrice,
            product.ProductCategoryId,
            product.Description
        );

        await _db.SaveChangesAsync();

        return Map(product);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        EnsureTenantContext();

        var product = await _db.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product is null)
            return false;

        _db.Set<Product>().Remove(product);

        await _db.SaveChangesAsync();

        return true;
    }

    private void EnsureTenantContext()
    {
        var tenantId = _tenantResolver.GetTenantId();

        if (string.IsNullOrWhiteSpace(tenantId))
        {
            throw new InvalidOperationException(
                "Tenant context is missing."
            );
        }
    }

    private static ProductDto Map(Product product)
    {
        return new ProductDto(
            product.Id,
            product.ProductCode,
            product.Name,
            product.SellingPrice,
            product.IsActive
        );
    }
}