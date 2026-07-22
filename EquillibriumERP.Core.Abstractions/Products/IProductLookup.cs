
namespace EquillibriumERP.Core.Abstractions.Products;

public interface IProductLookup
{
    Task<ProductLookupResult?> GetByIdAsync(Guid productId, CancellationToken cancellationToken = default);
}