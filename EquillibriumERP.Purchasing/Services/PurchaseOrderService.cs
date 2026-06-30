using EquillibriumERP.Purchasing.Interfaces;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Purchasing.Contracts;
using EquillibriumERP.Purchasing.Domain.Entities;
using EquillibriumERP.Purchasing.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Purchasing.Infrastructure.Persistence;
using EquillibriumERP.Core.Abstractions.Products;

namespace EquillibriumERP.Purchasing.Services;

public class PurchaseOrderService : IPurchaseOrderService
{
    private readonly PurchasingDbContext _db;
    private readonly IProductLookup _productLookup;
    private readonly ITenantContextualizer _tenantContextualizer;

    public PurchaseOrderService(
    PurchasingDbContext db,
    IProductLookup productLookup,
    ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _productLookup = productLookup;
        _tenantContextualizer = tenantContextualizer;
    }
    public async Task<PurchaseOrderResponse> CreateAsync(
    CreatePurchaseOrderRequest request,
    CancellationToken ct = default)
    {
        await _tenantContextualizer.SetTenantContextAsync(_db, ct);
        var supplier = await _db.Suppliers
            .FirstOrDefaultAsync(x => x.Id == request.SupplierId, ct);

        if (supplier == null)
            throw new InvalidOperationException(
                $"Supplier not found: {request.SupplierId}");

        var po = new PurchaseOrder
        {
            Id = Guid.NewGuid(),
            SupplierId = supplier.Id,
            OrderDateUtc = request.OrderDateUtc,
            Status = PurchaseOrderStatus.Draft,
            Number = $"PO-{DateTime.UtcNow:yyyyMMddHHmmss}"
        };

        foreach (var line in request.Lines)
        {
            await _tenantContextualizer.SetTenantContextAsync(_db, ct);
            var product = await _productLookup
                .GetByIdAsync(line.ProductId, ct);

            if (product == null)
                throw new InvalidOperationException(
                    $"Product not found: {line.ProductId}");

            po.Lines.Add(new PurchaseOrderLine
            {
                Id = Guid.NewGuid(),
                ProductId = line.ProductId,
                Quantity = line.Quantity,
                UnitPrice = product.SellingPrice,
                LineTotal = line.Quantity * product.SellingPrice
            });
        }
        //await _tenantContextualizer.SetTenantContextAsync(_db, ct);
        _db.PurchaseOrders.Add(po);

        await _db.SaveChangesAsync(ct);

        return new PurchaseOrderResponse
        {
            Id = po.Id,
            Number = po.Number,
            SupplierId = po.SupplierId,
            OrderDateUtc = po.OrderDateUtc,
            Status = po.Status
        };
    }
    
}