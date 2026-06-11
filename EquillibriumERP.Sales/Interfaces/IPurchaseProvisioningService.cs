using EquillibriumERP.Sales.Application.Contracts.Purchases;
namespace EquillibriumERP.Sales.Application.Interfaces;

public interface IPurchaseProvisioningService
{
    Task<Guid> CreatePurchaseOrderAsync(
        CreatePurchaseOrderRequest request,
        CancellationToken cancellationToken = default);
}