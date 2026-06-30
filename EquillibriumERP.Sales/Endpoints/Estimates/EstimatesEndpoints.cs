using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using EquillibriumERP.Sales.Application.Interfaces;
using EquillibriumERP.Sales.Application.Contracts.Estimates;
using EquillibriumERP.Sales.Application.Contracts.Invoices;
using EquillibriumERP.Sales.Application.Contracts.Purchases;
using EquillibriumERP.Sales.Services;
using EquillibriumERP.Sales.Contracts.Estimates;
using EquillibriumERP.Sales.Interfaces;

namespace EquillibriumERP.Sales.Endpoints;

public static class EstimatesEndpoints
{
    public static void MapEstimateEndpoints(IEndpointRouteBuilder app)
    {
        MapGetById(app);
        MapCreateEstimate(app);
        
    }

    private static void MapCreateEstimate(IEndpointRouteBuilder app)
    {
        app.MapPost("/estimate/create",
        async (
            CreateEstimateRequest request,
            IEstimateService service,
            CancellationToken ct) =>
        {
            var result = await service.CreateEstimateAsync(request, ct);

            return Results.Created($"/{result.Id}", result);
        });
    }

    private static void MapGetById(IEndpointRouteBuilder app)
    {
        app.MapGet("estimateBy/{id:guid}",
        async (
        Guid id,
        IEstimateService service,
        CancellationToken ct) =>
        {
            var estimate = await service.GetByIdAsync(id, ct);

            return estimate is null
                ? Results.NotFound()
                : Results.Ok(estimate);
        });
    }
}