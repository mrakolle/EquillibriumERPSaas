using EquillibriumERP.Core.Abstractions.Products;

using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Products.Services;

public class ProductLookupService : IProductLookup
{
    private readonly ProductsDbContext _db;

    public ProductLookupService(ProductsDbContext db)
    {
        _db = db;
    }

    public async Task<ProductLookupResult?> GetByIdAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
        {
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