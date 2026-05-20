using EquillibriumERP.Abstractions.Modules;
using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using EquillibriumERP.Sales.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using EquillibriumERP.Abstractions.Persistence;

namespace EquillibriumERP.Sales.Infrastructure;

public class SalesModule : IModule
{
    public string Name => "Sales";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        // future: application services, validators, etc.
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
        // EF configurations if needed
    }

    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/sales");

        MapOrders(group);
        MapProducts(group);
    }


    // ---------------------------
    // ORDERS (placeholder)
    // ---------------------------
    private static void MapOrders(RouteGroupBuilder group)
    {
        /*group.MapGet("/products", async (IRepository<Product> repo) =>
        {
            var products = await repo.GetAllAsync();

            return Results.Ok(products.Select(p => new
            {
                p.Id,
                p.ProductCode,
                p.Name,
                p.SellingPrice,
                p.IsActive
            }));
        });*/
    }

    // ---------------------------
    // PRODUCTS (TENANT AWARE)
    // ---------------------------
    private static void MapProducts(RouteGroupBuilder group)
    {
        
        group.MapGet("/products", async (
            [FromHeader(Name = "X-Tenant-Id")] string tenantId,
            ITenantDbContext db) =>
        {
            var products = await db.Set<Product>()
                .AsNoTracking()
                .Select(p => new
                {
                    p.Id,
                    p.ProductCode,
                    p.Name,
                    p.SellingPrice,
                    p.IsActive
                })
                .ToListAsync();

            return Results.Ok(products);
        });
        
        group.MapPost("/products",
        async (
            [FromHeader(Name = "X-Tenant-Id")] string tenantId,
            CreateProductRequest request,
            [FromServices] ITenantDbContext db) =>
        {
            Console.WriteLine($"HEADER TENANT = {tenantId}");

            var product = new Product(
                request.ProductCode,
                request.Name,
                request.ProductType,
                request.SellingPrice,
                request.CostPrice,
                request.ProductCategoryId,
                request.Description
            );

            db.Set<Product>().Add(product);

            await db.SaveChangesAsync();

            return Results.Ok(new
            {
                product.Id,
                product.ProductCode
            });
        });
        
    }
}

// ---------------------------
// DTOs
// ---------------------------
public record CreateSalesOrderRequest(string Code);

public record CreateProductRequest(
    string ProductCode,
    string Name,
    ProductType ProductType,
    decimal SellingPrice,
    decimal CostPrice,
    Guid? ProductCategoryId,
    string? Description
);