namespace EquillibriumERP.Sales.Application.Contracts.Purchases;

public sealed record CreatePurchaseOrderRequest(
    Guid SupplierId);