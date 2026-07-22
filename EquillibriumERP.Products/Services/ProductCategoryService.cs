using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Domain.Enums;
using EquillibriumERP.Products.Contracts;
using EquillibriumERP.Products.Domain.Entities;
using EquillibriumERP.Products.Interfaces;

namespace EquillibriumERP.Products.Services;

public class ProductCategoryService : IProductCategoryService
{
    private readonly ProductsDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    public ProductCategoryService(
        ProductsDbContext db,
        ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
    }

    public async Task<List<ProductCategoryDetailDto>> GetAllAsync()
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

        return await _db.ProductCategories
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => new ProductCategoryDetailDto(
                x.Id,
                x.Name,
                (int)x.ProductType,
                x.Description,
                x.IsActive
            ))
            .ToListAsync();
    }

    public async Task<ProductCategoryDetailDto?> GetByIdAsync(Guid id)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

        var category = await _db.ProductCategories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return category is null
            ? null
            : Map(category);
    }

    public async Task<ProductCategoryDetailDto> CreateAsync(
        CreateProductCategoryRequest dto,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var category = new ProductCategory(
            dto.Name,
            (ProductType)dto.ProductType,
            dto.Description
        );

        _db.ProductCategories.Add(category);

        await _db.SaveChangesAsync(ct);

        return Map(category);
    }

    public async Task<ProductCategoryDetailDto?> UpdateAsync(
        Guid id,
        UpdateProductCategoryRequest dto,
        CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);

        var category = await _db.ProductCategories
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (category is null)
            return null;

        //
        // Requires an Update() method on ProductCategory.
        //

        category.Update(
            dto.Name,
            (ProductType)dto.ProductType,
            dto.Description
        );

        if (dto.IsActive)
            category.Activate();
        else
            category.Deactivate();

        await _db.SaveChangesAsync(ct);

        return Map(category);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db);

        var category = await _db.ProductCategories
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category is null)
            return false;

        _db.ProductCategories.Remove(category);

        await _db.SaveChangesAsync();

        return true;
    }

    private static ProductCategoryDetailDto Map(ProductCategory category)
    {
        return new ProductCategoryDetailDto(
            category.Id,
            category.Name,
            (int)category.ProductType,
            category.Description,
            category.IsActive
        );
    }
}