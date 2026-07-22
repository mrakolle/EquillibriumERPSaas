using EquillibriumERP.Core.Abstractions.Products;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Products.Services;

public class ProductLookupService : IProductLookup
{
    private readonly ProductsDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    /*public ProductLookupService(ProductsDbContext db)
    {
        _db = db;
    }*/
    public ProductLookupService(ProductsDbContext db, ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
    }

    public async Task<ProductLookupResult?> GetByIdAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, cancellationToken);

        var entity = await _db.Products
            .FirstOrDefaultAsync(x => x.Id == productId, cancellationToken);

        Console.WriteLine($"Description from EF: '{entity?.Description}'");

        return entity is null
        ? null
        : new ProductLookupResult
        {
            Id = entity.Id,
            ProductCode = entity.ProductCode,
            Name = entity.Name,
            ProductType = entity.ProductType,
            ProductCategoryId = entity.ProductCategoryId,
            CasNumber = entity.CasNumber,
            Description = entity.Description,
            SellingPrice = entity.SellingPrice,
            CostPrice = entity.CostPrice,
            UnitOfMeasure = entity.UnitOfMeasure,
            TaxRate = entity.TaxRate,
            IsActive = entity.IsActive
        };
    }
}