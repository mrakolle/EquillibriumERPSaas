using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using EquillibriumERP.Core.Abstractions.Products;
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
    
         // Products
        MapCreateProduct(group);
        MapGetProductsById(group);
        MapGetAllProducts(group);
        MapUpdateProduct(group);

        // Product Categories
        MapGetProductCategories(group);
        MapGetProductCategoryById(group);
        MapCreateProductCategory(group);
        MapUpdateProductCategory(group);
        MapDeleteProductCategory(group);
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
            async (Guid id, IProductLookup lookup) =>
        {
            var product = await lookup.GetByIdAsync(id);

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
    private static void MapGetProductCategories(RouteGroupBuilder group)
    {
        group.MapGet("/product-categories/get-all", async (
            IProductCategoryService service) =>
        {
            var categories = await service.GetAllAsync();

            return Results.Ok(categories);
        });
    }
    private static void MapUpdateProduct(RouteGroupBuilder group)
    {
        group.MapPut("/update/{id:guid}", async (
            Guid id,
            [FromBody] UpdateProductRequest dto,
            HttpContext ctx,
            CancellationToken ct) =>
        {
            var service = ctx.RequestServices.GetRequiredService<IProductService>();
            var result = await service.UpdateAsync(id, dto, ct);
            return Results.Ok(result);
        });
        
    }

    ///--------------------------------------------------------------
    /// Product Categories Section
    /// -------------------------------------------------------------
    private static void MapGetProductCategoryById(RouteGroupBuilder group)
    {
        group.MapGet("/product-categories/get-by/{id:guid}",
            async (
                Guid id,
                IProductCategoryService service) =>
            {
                var category = await service.GetByIdAsync(id);

                return category is null
                    ? Results.NotFound()
                    : Results.Ok(category);
            });
    }

    private static void MapCreateProductCategory(RouteGroupBuilder group)
    {
        group.MapPost("/product-categories/create",
            async (
                [FromBody] CreateProductCategoryRequest dto,
                IProductCategoryService service,
                CancellationToken ct) =>
            {
                var result = await service.CreateAsync(dto, ct);

                return Results.Ok(result);
            });
    }

    private static void MapUpdateProductCategory(RouteGroupBuilder group)
    {
        group.MapPut("/product-categories/update/{id:guid}",
            async (
                Guid id,
                [FromBody] UpdateProductCategoryRequest dto,
                IProductCategoryService service,
                CancellationToken ct) =>
            {
                var result = await service.UpdateAsync(id, dto, ct);

                return result is null
                    ? Results.NotFound()
                    : Results.Ok(result);
            });
    }

    private static void MapDeleteProductCategory(RouteGroupBuilder group)
    {
        group.MapDelete("/product-categories/delete/{id:guid}",
            async (
                Guid id,
                IProductCategoryService service) =>
            {
                var deleted = await service.DeleteAsync(id);

                return deleted
                    ? Results.Ok()
                    : Results.NotFound();
            });
    }


}