using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using EquillibriumERP.Products.Application.DTOs;
using EquillibriumERP.Products.Application.Interfaces;

namespace EquillibriumERP.Products.Infrastructure.Endpoints;

public static class ProductProvisioningEndpoints
{
    public static void MapProductProvisioningEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products")
            .RequireAuthorization();

        MapCreateProduct(group);
        MapGetProductsById(group);
        MapGetAllProducts(group);
    }

    private static void MapGetAllProducts(RouteGroupBuilder group)
    {
        group.MapGet("/get-all",
            async (IProductService service) =>
        {
            var result = await service.GetAllAsync();

            return Results.Ok(result);
        });
    }

    private static void MapGetProductsById(RouteGroupBuilder group)
    {
        group.MapGet("/get-by/{id:guid}",
            async (Guid id, IProductService service) =>
        {
            var product = await service.GetByIdAsync(id);

            return product is null
                ? Results.NotFound()
                : Results.Ok(product);
        });
    }

    private static void MapCreateProduct(RouteGroupBuilder group)
    {
        group.MapPost("/create",
            async (CreateProductDto dto, IProductService service) =>
        {
            var result = await service.CreateAsync(dto);

            return Results.Ok(result);
        });
    }
}