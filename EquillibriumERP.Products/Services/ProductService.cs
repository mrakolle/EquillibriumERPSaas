using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Products.Contracts;
using EquillibriumERP.Products.Interfaces;
using EquillibriumERP.Products.Domain.Entities;
using EquillibriumERP.Core.Abstractions.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace EquillibriumERP.Products.Services;

public class ProductService : IProductService
{
    private readonly ProductsDbContext _db;
    private readonly ITenantResolver _tenantResolver;
    private readonly ITenantSession _tenantSession;
    private readonly ITenantContextualizer _tenantContextualizer;

    public ProductService(
        ProductsDbContext db,
        ITenantResolver tenantResolver, ITenantContextualizer tenantContextualizer, ITenantSession tenantSession)
    {
        _db = db;
        _tenantResolver = tenantResolver;
        _tenantContextualizer = tenantContextualizer;
        _tenantSession = tenantSession;
        
    }

    public async Task<ProductDto> CreateAsync(
    CreateProductRequest dto,
    CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var product = new Product(
            dto.ProductCode,
            dto.Name,
            dto.ProductType,
            dto.SellingPrice,
            dto.CostPrice,
            dto.ProductCategoryId,
            dto.CasNumber,
            dto.Description
            
            
            
        );

        if (!dto.IsActive)
            product.Deactivate();

        _db.Products.Add(product);

        await _db.SaveChangesAsync(ct);

        return Map(product);
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        CancellationToken ct = default;
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);
        return await _db.Set<Product>()
            .AsNoTracking()
            .Select(p => Map(p))
            .ToListAsync();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        CancellationToken ct = default;
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var product = await _db.Set<Product>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return product is null
            ? null
            : Map(product);
    }

    public async Task<ProductDto?> UpdateAsync(
        Guid id,
        UpdateProductRequest dto, CancellationToken ct = default)
    {
        
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var product = await _db.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (product is null)
            return null;

        product.Update(
        dto.Name,
        dto.ProductCode,
        dto.ProductType,
        dto.ProductCategoryId,
        dto.SellingPrice,
        dto.CostPrice,
        dto.CasNumber,
        dto.Description
    );

        if (dto.IsActive)
            product.Activate();
        else
        product.Deactivate();

        await _db.SaveChangesAsync();

        return Map(product);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        CancellationToken ct = default;
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

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
        product.CasNumber,
        product.Description,
        product.ProductType,
        product.SellingPrice,
        product.IsActive
    );
    }
}