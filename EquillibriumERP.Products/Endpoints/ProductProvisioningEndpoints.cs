using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Core.Infrastructure.Authorization;
using EquillibriumERP.Products.Infrastructure;
using EquillibriumERP.Products.Contracts;
using EquillibriumERP.Products.Interfaces;

namespace EquillibriumERP.Products.Infrastructure.Endpoints;

public static class ProductProvisioningEndpoints
{
    public static void MapProductProvisioningEndpoints(RouteGroupBuilder group)
    {
    
        MapCreateProduct(group);
        MapGetProductsById(group);
        MapGetAllProducts(group);
    }

    private static void MapGetAllProducts(RouteGroupBuilder group)
    {
        
        group.MapGet("/get-all", async (HttpContext ctx) =>
        {
            var service = ctx.RequestServices.GetRequiredService<IProductService>();
            var result = await service.GetAllAsync();

            return Results.Ok(result);
        });
        //.RequireAuthorization("products.view");
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
       group.MapPost("/create", async (
            [FromBody] CreateProductRequest dto,
            HttpContext ctx, CancellationToken ct) =>
        {
            var service = ctx.RequestServices.GetRequiredService<IProductService>();
            var result = await service.CreateAsync(dto,ct);

            return Results.Ok(result);
        });
       //.RequireAuthorization("perm:products.create");
    }
}