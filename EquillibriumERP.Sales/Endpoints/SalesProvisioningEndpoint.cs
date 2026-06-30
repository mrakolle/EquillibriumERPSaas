using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using EquillibriumERP.Sales.Application.Interfaces;
using EquillibriumERP.Sales.Contracts.Estimates;
//using EquillibriumERP.Sales.Contracts.Invoices;
//using EquillibriumERP.Sales.Contracts.Purchases;
using EquillibriumERP.Sales.Services;

namespace EquillibriumERP.Sales.Endpoints;

public static class SalesProvisioningEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        /*var sales = app.MapGroup("/sales")
            .WithTags("Sales");

        var purchases = app.MapGroup("/purchases")
            .WithTags("Purchases");

        // =========================
        // SALES - ESTIMATES
        // =========================

        sales.MapPost("/estimates", async (
            CreateEstimateRequest request,
            EstimateService service,
            CancellationToken ct) =>
        {
            var estimateId = await service.CreateEstimateAsync(request, ct);
            return Results.Ok(new { EstimateId = estimateId });
        });

        // =========================
        // SALES - INVOICES
        // =========================

       /* sales.MapPost("/invoices", async (
            CreateInvoiceRequest request,
            ISalesService service,
            CancellationToken ct) =>
        {
            var invoiceId = await service.CreateInvoiceAsync(request, ct);
            return Results.Ok(new { InvoiceId = invoiceId });
        });*/

        // =========================
        // PURCHASES - ORDERS
        // =========================

        /*purchases.MapPost("/orders", async (
            CreatePurchaseOrderRequest request,
            IPurchaseProvisioningService service,
            CancellationToken ct) =>
        {
            var orderId = await service.CreatePurchaseOrderAsync(request, ct);
            return Results.Ok(new { PurchaseOrderId = orderId });
        });*/
    }
}