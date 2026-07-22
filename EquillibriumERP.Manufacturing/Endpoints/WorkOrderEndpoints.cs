using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.Manufacturing.Interfaces;
using EquillibriumERP.Manufacturing.Contracts;
using EquillibriumERP.Manufacturing.Services;


namespace EquillibriumERP.Manufacturing.Endpoints;

public static class WorkOrderEndpoints
{
    public static void MapWorkOrderEndpoints(RouteGroupBuilder group)
    {
        MapCreate(group);
        MapGetAll(group);
        MapGetById(group);
        MapStartWorkOrder(group);
        MapStartStep(group);
        MapExecuteStep(group);
        //MapCompleteStep(group);
        
    }

    // ---------------- CREATE ----------------
    private static void MapCreate(RouteGroupBuilder group)
    {
        group.MapPost("/workorders/create", async (
            CreateWorkOrderRequest request,
            IWorkOrderService service, CancellationToken ct) =>
        {
            var id = await service.CreateWorkOrderAsync(request, ct);
            return Results.Ok(id);
        });
    }

    // ---------------- GET ALL ----------------
    private static void MapGetAll(RouteGroupBuilder group)
    {
        group.MapGet("/workorders/GetAll", async (
            IWorkOrderService service,
            CancellationToken ct) =>
        {
            var result = await service.GetAllAsync(ct);

            return Results.Ok(result);
        });
    }

    // ---------------- GET BY ID ----------------
    private static void MapGetById(RouteGroupBuilder group)
    {
        group.MapGet("/workorders/{id:guid}", async (
            Guid id,
            IWorkOrderService service,
            CancellationToken ct) =>
        {
            var result = await service.GetByIdAsync(id, ct);

            return result is null
                ? Results.NotFound()
                : Results.Ok(result);
        });
    }

    // ---------------- START STEP ----------------
    private static void MapStartStep(RouteGroupBuilder group)
    {
       group.MapPost(
        "/workorders/{workOrderId:guid}/steps/{stepId:guid}/start",
        async (
            Guid workOrderId,
            Guid stepId,
            IWorkOrderService service,
            CancellationToken ct) =>
        {
            await service.StartStepAsync(workOrderId, stepId, ct);
            return Results.Ok();
        });
    }

    // ---------------- COMPLETE STEP ----------------
    private static void MapExecuteStep(RouteGroupBuilder group)
    {
        group.MapPost("/workorders/{workOrderId}/steps/{stepId}/execute",
            async (
                Guid workOrderId,
                Guid stepId,
                ExecuteStepRequest request,
                IWorkOrderService service,
                CancellationToken ct) =>
            {
                await service.ExecuteStepAsync(workOrderId, stepId, request, ct);

                return Results.Ok();
            });
    }

    // ---------------- CONSUME MATERIAL ----------------
    private static void MapStartWorkOrder(RouteGroupBuilder group)
    {
       group.MapPost("/workorders/{id:guid}/start",
        async (Guid id, IWorkOrderService service, CancellationToken ct) =>
        {
            await service.StartWorkOrderAsync(id, ct);
            return Results.Ok();
        });
    }
}