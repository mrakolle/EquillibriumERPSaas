using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.Purchasing.Contracts;
using EquillibriumERP.Purchasing.Interfaces;

namespace EquillibriumERP.Purchasing.Endpoints;

public static class PurchaseOrderEndpoints
{
    public static void MapPurchaseOrderEndpoints(RouteGroupBuilder group)
    {
        group.MapGroup("/api/purchasing/purchase-orders")
            .WithTags("Purchasing - Purchase Orders");

        MapCreatePurchaseOrder(group);

    }

    private static void MapCreatePurchaseOrder(RouteGroupBuilder group)
    {
        group.MapPost("PurchaseOrders/Create", async (
            CreatePurchaseOrderRequest request,
            IPurchaseOrderService service,
            CancellationToken ct) =>
        {
            var result = await service.CreateAsync(request, ct);

            return Results.Ok(result);
        });
    }
}