using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using EquillibriumERP.Sales.Application.Interfaces;
using EquillibriumERP.Sales.Application.Contracts;
using EquillibriumERP.Sales.Application.Contracts.Invoices;
using EquillibriumERP.Sales.Application.Contracts.Purchases;
using EquillibriumERP.Sales.Services;
using EquillibriumERP.Sales.Contracts;
using EquillibriumERP.Sales.Interfaces;

namespace EquillibriumERP.Sales.Endpoints;

public static class EstimatesEndpoints
{
    public static void MapEstimateEndpoints(RouteGroupBuilder group)
    {
        MapCreateEstimate(group);
        MapGetAllEstimates(group);
        MapGetEstimateById(group);
        MapUpdateEstimate(group);
        // MapDeleteEstimate(group);
    }

    private static void MapCreateEstimate(RouteGroupBuilder group)
    {
        group.MapPost("/estimates/create", async (
            CreateEstimateRequest request,
            IEstimateService service,
            CancellationToken ct) =>
        {
            var estimate = await service.CreateAsync(request, ct);

            return Results.Ok(estimate);
        });
    }
    private static void MapGetAllEstimates(RouteGroupBuilder group)
    {
        group.MapGet("/estimates", async (
            IEstimateService service,
            CancellationToken ct) =>
        {
            var estimates = await service.GetAllAsync(ct);

            return Results.Ok(estimates);
        });
    }
    private static void MapGetEstimateById(RouteGroupBuilder group)
    {
        group.MapGet("/estimates/{id:guid}", async (
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
    private static void MapUpdateEstimate(RouteGroupBuilder group)
    {
        group.MapPut("/estimates/{id:guid}", async (
            Guid id,
            UpdateEstimateRequest request,
            IEstimateService service,
            CancellationToken ct) =>
        {
            var estimate = await service.UpdateAsync(id, request, ct);

            return Results.Ok(estimate);
        });
    }
}