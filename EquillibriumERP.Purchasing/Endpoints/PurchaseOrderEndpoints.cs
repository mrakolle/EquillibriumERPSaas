using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Purchasing.Domain.Entities;
using EquillibriumERP.Core.Abstractions.Products;
using EquillibriumERP.Purchasing.Contracts;
using EquillibriumERP.Purchasing.Domain.Enums;

namespace EquillibriumERP.Purchasing.Endpoints;

public static class PurchaseOrderEndpoints
{
    public static void MapPurchaseOrderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/purchasing/purchase-orders")
            .WithTags("Purchasing - Purchase Orders");

        MapCreatePurchaseOrder(group);

    }

    private static void MapCreatePurchaseOrder(Microsoft.AspNetCore.Routing.RouteGroupBuilder group)
    {
        group.MapPost("/", async (
            CreatePurchaseOrderRequest request,
            PurchasingDbContext db,
            IProductLookup productLookup,
            CancellationToken ct) =>
        {
            var supplier = await db.Suppliers
                .FirstOrDefaultAsync(x => x.Id == request.SupplierId, ct);

            if (supplier == null)
                return Results.NotFound("Supplier not found");

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
                var product = await productLookup.GetByIdAsync(line.ProductId, ct);

                if (product == null)
                    return Results.NotFound($"Product not found: {line.ProductId}");

                po.Lines.Add(new PurchaseOrderLine
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Quantity = line.Quantity,
                    UnitPrice = product.SellingPrice,
                    LineTotal = line.Quantity * product.SellingPrice
                });
            }

            db.PurchaseOrders.Add(po);
            await db.SaveChangesAsync(ct);

            return Results.Ok(new PurchaseOrderResponse
            {
                Id = po.Id,
                Number = po.Number,
                SupplierId = po.SupplierId,
                OrderDateUtc = po.OrderDateUtc,
                Status = po.Status
            });
        });
    }
}