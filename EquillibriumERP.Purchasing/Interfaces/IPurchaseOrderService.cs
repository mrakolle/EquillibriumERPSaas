using EquillibriumERP.Purchasing.Contracts;

namespace EquillibriumERP.Purchasing.Interfaces;
public interface IPurchaseOrderService
{
    Task<PurchaseOrderResponse> CreateAsync(
        CreatePurchaseOrderRequest request,
        CancellationToken ct = default);
}