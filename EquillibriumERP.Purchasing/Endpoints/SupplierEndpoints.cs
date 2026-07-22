using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using EquillibriumERP.Purchasing.Contracts;
using EquillibriumERP.Purchasing.Interfaces;

namespace EquillibriumERP.Purchasing.Endpoints;

public static class SupplierEndpoints
{
    public static void MapSupplierEndpoints(RouteGroupBuilder group)
    {
        MapCreateSupplier(group);
        MapGetAllSuppliers(group);
        MapGetSupplierById(group);
        MapUpdateSupplier(group);
        //MapDeleteSupplier(group);
    }

    private static void MapCreateSupplier(RouteGroupBuilder group)
    {
        group.MapPost("/suppliers/create", async (
            CreateSupplierRequest request,
            ISupplierService service,
            CancellationToken ct) =>
        {
            var supplier = await service.CreateAsync(request, ct);

            return Results.Ok(supplier);
        });
    }

    private static void MapGetAllSuppliers(RouteGroupBuilder group)
    {
        group.MapGet("/suppliers/get-all", async (
            ISupplierService service) =>
        {
            var suppliers = await service.GetAllAsync();

            return Results.Ok(suppliers);
        });
    }

    private static void MapGetSupplierById(RouteGroupBuilder group)
    {
        group.MapGet("/suppliers/get-by/{id:guid}", async (
            Guid id,
            ISupplierService service) =>
        {
            var supplier = await service.GetByIdAsync(id);

            if (supplier is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(supplier);
        });
    }

    private static void MapUpdateSupplier(RouteGroupBuilder group)
    {
        group.MapPut("/suppliers/update/{id:guid}", async (
            Guid id,
            UpdateSupplierRequest request,
            ISupplierService service) =>
        {
            var supplier = await service.UpdateAsync(id, request);

            if (supplier is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(supplier);
        });
    }

    /*
    private static void MapDeleteSupplier(RouteGroupBuilder group)
    {
        group.MapDelete("/suppliers/delete/{id:guid}", async (
            Guid id,
            ISupplierService service) =>
        {
            var deleted = await service.DeleteAsync(id);

            return deleted
                ? Results.Ok()
                : Results.NotFound();
        });
    }
    */
}