using EquillibriumERP.Sales.Contracts;
using EquillibriumERP.Sales.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EquillibriumERP.Sales.Endpoints;

public static class CustomerAddressEndpoints
{
    public static void MapCustomerAddressEndpoints(RouteGroupBuilder group)
    {
        MapGetAll(group);
        MapGetById(group);
        MapCreate(group);
        MapUpdate(group);
        MapDelete(group);
    }

    private static void MapGetAll(RouteGroupBuilder group)
    {
        group.MapGet("/customer-addresses/get-all/{customerId:guid}",
            async (
                Guid customerId,
                ICustomerAddressService service) =>
            {
                var addresses =
                    await service.GetAllAsync(customerId);

                return Results.Ok(addresses);
            });
    }

    private static void MapGetById(RouteGroupBuilder group)
    {
        group.MapGet("/customer-addresses/get-by/{id:guid}",
            async (
                Guid id,
                ICustomerAddressService service) =>
            {
                var address =
                    await service.GetByIdAsync(id);

                return address is null
                    ? Results.NotFound()
                    : Results.Ok(address);
            });
    }

    private static void MapCreate(RouteGroupBuilder group)
    {
        group.MapPost("/customer-addresses/create",
            async (
                CreateCustomerAddressRequest request,
                ICustomerAddressService service,
                CancellationToken ct) =>
            {
                var address =
                    await service.CreateAsync(request, ct);

                return Results.Ok(address);
            });
    }

    private static void MapUpdate(RouteGroupBuilder group)
    {
        group.MapPut("/customer-addresses/update/{id:guid}",
            async (
                Guid id,
                UpdateCustomerAddressRequest request,
                ICustomerAddressService service,
                CancellationToken ct) =>
            {
                var address =
                    await service.UpdateAsync(id, request, ct);

                return Results.Ok(address);
            });
    }

    private static void MapDelete(RouteGroupBuilder group)
    {
        group.MapDelete("/customer-addresses/delete/{id:guid}",
            async (
                Guid id,
                ICustomerAddressService service,
                CancellationToken ct) =>
            {
                await service.DeleteAsync(id, ct);

                return Results.Ok();
            });
    }
}