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
        return await _db.Products
        .Where(x => x.Id == productId)
        .Select(x => new ProductLookupResult
        {
            Id = x.Id,
            Code = x.ProductCode,
            Name = x.Name,
            SellingPrice = x.SellingPrice,
            IsActive = x.IsActive
        })
        .FirstOrDefaultAsync(cancellationToken);
    }
}