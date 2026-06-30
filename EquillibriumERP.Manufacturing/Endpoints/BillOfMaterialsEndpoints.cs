using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.Manufacturing.Interfaces;
using EquillibriumERP.Manufacturing.Contracts;

namespace EquillibriumERP.Manufacturing.Endpoints;

public static class BillOfMaterialsEndpoints
{
    public static void MapBillOfMaterialsEndpoints(RouteGroupBuilder group)
    {
        MapCreateBom(group);
        MapAddBomSteps(group);
        //MapAddBomStepMaterial(group);
        //MapGetExpecteBomMaterialQuantity(group);
        //MapConsumeStepMaterial(group);
        //MapGetBomVariance(group);
        //MapGetAllBoms(group);
        //MapGetBomById(group);
    }

    private static void MapCreateBom(RouteGroupBuilder group)
    {
        group.MapPost("/bom/Create", async (
            CreateBomRequest request,
            IBomService service,
            CancellationToken ct) =>
        {
            var id = await service.CreateAsync(request, ct);
            return Results.Ok(id);
        });
    }
    private static void MapAddBomSteps(RouteGroupBuilder group)
    {
        group.MapPost("/bom/{bomId:guid}/steps", async (
            Guid bomId,
            CreateBOMStepRequest request,
            IBomService service,
            CancellationToken ct) =>
        {
            var id = await service.AddStepAsync(bomId, request, ct);
            return Results.Ok(id);
        });
    }

    private static void MapAddBomStepMaterial(RouteGroupBuilder group)
    {
        
        group.MapPost("/bom/steps/{stepId:guid}/materials", async (
            Guid stepId,
            CreateBOMStepMaterialRequest request,
            IBomService service,
            CancellationToken ct) =>
        {
            var id = await service.AddStepMaterialAsync(stepId, request, ct);
            return Results.Ok(id);
        });
    }
    private static void MapGetExpecteBomMaterialQuantity(RouteGroupBuilder group)
    {
        group.MapGet("/bom/steps/{stepId}/expected", async (
            Guid stepId,
            IBomService service,
            CancellationToken ct) =>
        {
            var result = await service.GetStepExpectedAsync(stepId, ct);
            return Results.Ok(result);
        });
    }
    private static void MapConsumeStepMaterial(RouteGroupBuilder group)
    {
        group.MapPost("/bom/steps/{stepId}/consume", async (
            Guid stepId,
            RecordStepConsumptionRequest request,
            IBomService service,
            CancellationToken ct) =>
        {
            var id = await service.RecordConsumptionAsync(stepId, request, ct);
            return Results.Ok(id);
        });
    }
    private static void MapGetBomVariance(RouteGroupBuilder group)
    {
        group.MapGet("/bom/steps/{stepId}/variance", async (
            Guid stepId,
            IBomService service,
            CancellationToken ct) =>
        {
            var result = await service.GetStepVarianceAsync(stepId, ct);
            return Results.Ok(result);
        });
    }
    private static void MapGetAllBoms(RouteGroupBuilder group)
    {
        group.MapGet("/bom/GetAll", async (
            IBomService service,
            CancellationToken ct) =>
        {
            return await service.GetAllAsync(ct);
        });
    }

    private static void MapGetBomById(RouteGroupBuilder group)
    {
        group.MapGet("/bom/{id:guid}", async (
            Guid id,
            IBomService service,
            CancellationToken ct) =>
        {
            var result = await service.GetByIdAsync(id, ct);

            return result is null
                ? Results.NotFound()
                : Results.Ok(result);
        });
    }
}