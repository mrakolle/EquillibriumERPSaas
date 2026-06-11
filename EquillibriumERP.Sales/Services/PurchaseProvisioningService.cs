using EquillibriumERP.Sales.Application.Interfaces;
using EquillibriumERP.Sales.Application.Contracts.Purchases;

namespace EquillibriumERP.Sales.Services;

public sealed class PurchaseProvisioningService
    : IPurchaseProvisioningService
{
    public Task<Guid> CreatePurchaseOrderAsync(
        CreatePurchaseOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Guid.NewGuid());
    }
}