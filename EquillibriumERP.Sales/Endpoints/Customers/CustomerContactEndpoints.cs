using EquillibriumERP.Sales.Contracts;
using EquillibriumERP.Sales.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EquillibriumERP.Sales.Endpoints;

public static class CustomerContactEndpoints
{
    public static void MapCustomerContactEndpoints(RouteGroupBuilder group)
    {
        MapGetAll(group);
        MapGetById(group);
        MapCreate(group);
        MapUpdate(group);
        MapDelete(group);
    }

    private static void MapGetAll(RouteGroupBuilder group)
    {
        group.MapGet("/customer-contacts/get-all/{customerId:guid}",
            async (
                Guid customerId,
                ICustomerContactService service) =>
            {
                var contacts =
                    await service.GetAllAsync(customerId);

                return Results.Ok(contacts);
            });
    }

    private static void MapGetById(RouteGroupBuilder group)
    {
        group.MapGet("/customer-contacts/get-by/{id:guid}",
            async (
                Guid id,
                ICustomerContactService service) =>
            {
                var contact =
                    await service.GetByIdAsync(id);

                return contact is null
                    ? Results.NotFound()
                    : Results.Ok(contact);
            });
    }

    private static void MapCreate(RouteGroupBuilder group)
    {
        group.MapPost("/customer-contacts/create",
            async (
                CreateCustomerContactRequest request,
                ICustomerContactService service,
                CancellationToken ct) =>
            {
                var contact =
                    await service.CreateAsync(request, ct);

                return Results.Ok(contact);
            });
    }

    private static void MapUpdate(RouteGroupBuilder group)
    {
        group.MapPut("/customer-contacts/update/{id:guid}",
            async (
                Guid id,
                UpdateCustomerContactRequest request,
                ICustomerContactService service,
                CancellationToken ct) =>
            {
                var contact =
                    await service.UpdateAsync(id, request, ct);

                return Results.Ok(contact);
            });
    }

    private static void MapDelete(RouteGroupBuilder group)
    {
        group.MapDelete("/customer-contacts/delete/{id:guid}",
            async (
                Guid id,
                ICustomerContactService service,
                CancellationToken ct) =>
            {
                await service.DeleteAsync(id, ct);

                return Results.Ok();
            });
    }
}