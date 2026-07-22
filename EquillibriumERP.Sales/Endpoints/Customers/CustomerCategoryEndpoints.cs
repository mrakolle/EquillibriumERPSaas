using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using EquillibriumERP.Sales.Contracts;
using EquillibriumERP.Sales.Interfaces;

namespace EquillibriumERP.Sales.Endpoints;

public static class CustomerCategoryEndpoints
{
    public static void MapCustomerCategoryEndpoints(RouteGroupBuilder group)
    {
        MapCreateCustomerCategory(group);
        MapGetAllCustomerCategories(group);
        MapGetCustomerCategoryById(group);
        MapUpdateCustomerCategory(group);
        //MapDeleteCustomerCategory(group);
    }


    private static void MapCreateCustomerCategory(
        RouteGroupBuilder group)
    {
        group.MapPost(
            "/customer-categories/create",
            async (
                CreateCustomerCategoryRequest request,
                ICustomerCategoryService service,
                CancellationToken ct) =>
            {
                var category =
                    await service.CreateAsync(
                        request,
                        ct);

                return Results.Ok(category);
            });
    }


    private static void MapGetAllCustomerCategories(
        RouteGroupBuilder group)
    {
        group.MapGet(
            "/customer-categories/get-all",
            async (
                ICustomerCategoryService service) =>
            {
                var categories =
                    await service.GetAllAsync();

                return Results.Ok(categories);
            });
    }


    private static void MapGetCustomerCategoryById(
        RouteGroupBuilder group)
    {
        group.MapGet(
            "/customer-categories/get-by/{id:guid}",
            async (
                Guid id,
                ICustomerCategoryService service) =>
            {
                var category =
                    await service.GetByIdAsync(id);

                if (category is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(category);
            });
    }


    private static void MapUpdateCustomerCategory(
        RouteGroupBuilder group)
    {
        group.MapPut(
            "/customer-categories/update/{id:guid}",
            async (
                Guid id,
                UpdateCustomerCategoryRequest request,
                ICustomerCategoryService service,
                CancellationToken ct) =>
            {
                var category =
                    await service.UpdateAsync(
                        id,
                        request,
                        ct);

                if (category is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(category);
            });
    }


    /*
    private static void MapDeleteCustomerCategory(
        RouteGroupBuilder group)
    {
        group.MapDelete(
            "/customer-categories/delete/{id:guid}",
            async (
                Guid id,
                ICustomerCategoryService service) =>
            {
                var deleted =
                    await service.DeleteAsync(id);

                return deleted
                    ? Results.Ok()
                    : Results.NotFound();
            });
    }
    */
}