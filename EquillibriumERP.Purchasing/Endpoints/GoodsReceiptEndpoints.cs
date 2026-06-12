using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Purchasing.Domain.Entities;
using EquillibriumERP.Purchasing.Domain.Enums;

namespace EquillibriumERP.Purchasing.Endpoints;

public static class GoodsReceiptEndpoints
{
    public static void MapGoodsReceiptEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/purchasing/goods-receipts")
            .WithTags("Purchasing - Goods Receipts");

        group.MapPost("/from-po/{purchaseOrderId:guid}", async (
            Guid purchaseOrderId,
            PurchasingDbContext db,
            CancellationToken ct) =>
        {
            var po = await db.PurchaseOrders
                .Include(x => x.Lines)
                .FirstOrDefaultAsync(x => x.Id == purchaseOrderId, ct);

            if (po == null)
                return Results.NotFound("Purchase Order not found");

            if (po.Status != PurchaseOrderStatus.Approved)
                return Results.BadRequest("Only Approved POs can be received");

            var grn = new GoodsReceipt
            {
                Id = Guid.NewGuid(),
                PurchaseOrderId = po.Id,
                ReceivedDateUtc = DateTime.UtcNow,
                Status = "Draft",
                Number = $"GRN-{DateTime.UtcNow:yyyyMMddHHmmss}"
            };

            foreach (var line in po.Lines)
            {
                var remainingQty = line.Quantity - line.QuantityReceived;

                if (remainingQty <= 0)
                    continue;
                
                 // 🔒 SAFETY CHECK
                if (line.QuantityReceived > line.Quantity)
                    return Results.BadRequest("Over-receipt detected");

                grn.Lines.Add(new GoodsReceiptLine
                {
                    Id = Guid.NewGuid(),
                    ProductId = line.ProductId,
                    PurchaseOrderLineId = line.Id,
                    QuantityReceived = remainingQty,
                    SupplierLotNo = null
                });

                line.QuantityReceived += remainingQty;
            }

            db.GoodsReceipts.Add(grn);
            await db.SaveChangesAsync(ct);

            return Results.Ok(new
            {
                grn.Id,
                grn.Number,
                grn.PurchaseOrderId,
                grn.ReceivedDateUtc,
                grn.Status
            });
        });
    }
}