using EquillibriumERP.Core.Abstractions.Inventory;
using EquillibriumERP.Inventory.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EquillibriumERP.Inventory.Endpoints;

public static class InventoryEndpoints
{
    public static void MapInventoryEndpoints(
        RouteGroupBuilder group)
    {
       // var group = app.MapGroup("/inventory");

        group.MapPost("/items",
            async (InventoryDbContext db) =>
            {
                var items = await db.InventoryItems
                    .Select(x => new InventoryItemResponse
                    {
                        ProductId = x.ProductId,
                        QuantityOnHand = x.QuantityOnHand,
                        QuantityReserved = x.QuantityReserved,
                        ReorderLevel = x.ReorderLevel
                    })
                    .ToListAsync();

                return Results.Ok(items);
            });

        group.MapGet("/items/{productId:guid}",
            async (
                Guid productId,
                InventoryDbContext db) =>
            {
                var item = await db.InventoryItems
                    .Where(x => x.ProductId == productId)
                    .Select(x => new InventoryItemResponse
                    {
                        ProductId = x.ProductId,
                        QuantityOnHand = x.QuantityOnHand,
                        QuantityReserved = x.QuantityReserved,
                        ReorderLevel = x.ReorderLevel
                    })
                    .FirstOrDefaultAsync();

                return item is null
                    ? Results.NotFound()
                    : Results.Ok(item);
            });

        group.MapPost("/receive",
            async (
                ReceiveStockRequest request,
                IStockMovementService stockService,
                CancellationToken ct) =>
            {
                await stockService.ReceiveStockAsync(
                    new StockReceiveRequest
                    {
                        ProductId = request.ProductId,
                        Quantity = request.Quantity,
                        LotNo = request.LotNo,
                        ReferenceType = request.ReferenceType,
                        ReferenceId = request.ReferenceId
                    },
                    ct);

                return Results.Ok();
            });
    }
}