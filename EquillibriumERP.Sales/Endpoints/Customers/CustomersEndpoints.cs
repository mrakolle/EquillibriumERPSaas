using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using EquillibriumERP.Sales.Contracts;
using EquillibriumERP.Sales.Interfaces;

namespace EquillibriumERP.Sales.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(RouteGroupBuilder group)
    {
        MapCreateCustomer(group);
        MapGetAllCustomers(group);
        MapGetCustomerById(group);
        MapUpdateCustomer(group);
        //MapDeleteCustomer(group);
    }

    private static void MapCreateCustomer(RouteGroupBuilder group)
    {
        group.MapPost("/customers/create", async (
            CreateCustomerRequest request,
            ICustomerService service,
            CancellationToken ct) =>
        {
            var customer = await service.CreateAsync(request, ct);

            return Results.Ok(customer);
        });
    }

    private static void MapGetAllCustomers(RouteGroupBuilder group)
    {
        group.MapGet("/customers/get-all", async (
            ICustomerService service) =>
        {
            var customers = await service.GetAllAsync();

            return Results.Ok(customers);
        });
    }

    private static void MapGetCustomerById(RouteGroupBuilder group)
    {
        group.MapGet("/customers/get-by/{id:guid}", async (
            Guid id,
            ICustomerService service) =>
        {
            var customer = await service.GetByIdAsync(id);

            if (customer is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(customer);
        });
    }

    private static void MapUpdateCustomer(RouteGroupBuilder group)
    {
        group.MapPut("/customers/update/{id:guid}", async (
            Guid id,
            UpdateCustomerRequest request,
            ICustomerService service,
            CancellationToken ct) =>
        {
            var customer = await service.UpdateAsync(id, request, ct);

            if (customer is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(customer);
        });
    }

    /*
    private static void MapDeleteCustomer(RouteGroupBuilder group)
    {
        group.MapDelete("/customers/delete/{id:guid}", async (
            Guid id,
            ICustomerService service) =>
        {
            var deleted = await service.DeleteAsync(id);

            return deleted
                ? Results.Ok()
                : Results.NotFound();
        });
    }
    */
}