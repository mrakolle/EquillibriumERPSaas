using EquillibriumERP.Abstractions.Modules;
using EquillibriumERP.Sales.Domain.Entities;
using EquillibriumERP.Sales.Application.Features.Invoices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using EquillibriumERP.Sales.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using EquillibriumERP.Sales.Infrastructure.Requests;
using System.Data;
using EquillibriumERP.Abstractions.Persistence;
using EquillibriumERP.Sales.Infrastructure.Services;

namespace EquillibriumERP.Sales.Infrastructure;

public class SalesModule : IModule
{
    public string Name => "Sales";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<InvoiceNumberGenerator>();
        services.AddScoped<EstimateNumberGenerator>();
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
        // EF configurations if needed
    }

    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/sales");

        MapProducts(group);
        MapEstimates(group);
        MapInvoices(group);
        MapOrders(group);
    }


    // ---------------------------
    // ESTIMATES (placeholder)
    // ---------------------------
    private static void MapEstimates(RouteGroupBuilder group)
    {
        group.MapGet("/estimates/{id:guid}",
        async (
            Guid id,
            [FromHeader(Name = "X-Tenant-Id")] string tenantId,
            ITenantDbContext db) =>
        {
            var estimate = await db.Set<Estimate>()
                .Include(x => x.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (estimate is null)
                return Results.NotFound();

            return Results.Ok(new
            {
                estimate.Id,
                estimate.EstimateNumber,
                estimate.CustomerId,
                estimate.EstimateDateUtc,
                estimate.ExpiryDateUtc,
                estimate.Status,
                estimate.Subtotal,
                estimate.TaxAmount,
                estimate.TotalAmount,
                estimate.Notes,
                Items = estimate.Items.Select(i => new
                {
                    i.Id,
                    i.ProductId,
                    i.Description,
                    i.Quantity,
                    i.UnitPrice,
                    i.TaxRate,
                    i.LineSubtotal,
                    i.TaxAmount,
                    i.LineTotal
                })
            });
        });

        group.MapGet("/estimates",
        async (
            [FromHeader(Name = "X-Tenant-Id")] string tenantId,
            ITenantDbContext db) =>
        {
            var estimates = await db.Set<Estimate>()
                .AsNoTracking()
                .OrderByDescending(x => x.EstimateDateUtc)
                .Select(x => new
                {
                    x.Id,
                    x.EstimateNumber,
                    x.CustomerId,
                    x.EstimateDateUtc,
                    x.ExpiryDateUtc,
                    x.Status,
                    x.TotalAmount
                })
                .ToListAsync();

            return Results.Ok(estimates);
        });

        group.MapPut("/estimates/{id:guid}",
        async (
            Guid id,
            [FromHeader(Name = "X-Tenant-Id")] string tenantId,
            UpdateEstimateRequest request,
            ITenantDbContext db) =>
        {
            var estimate = await db.Set<Estimate>()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (estimate is null)
                return Results.NotFound();

            estimate.UpdateHeader(
                request.CustomerId,
                request.ExpiryDateUtc,
                request.Notes
            );

            var newItems = request.Items.Select(i =>
                new EstimateItem(
                    i.ProductId,
                    i.Description,
                    i.Quantity,
                    i.UnitPrice,
                    i.TaxRate
                )
            ).ToList();

            estimate.ReplaceItems(newItems);

            await db.SaveChangesAsync();

            return Results.Ok(new
            {
                estimate.Id,
                estimate.EstimateNumber,
                estimate.TotalAmount
            });
        });

        group.MapPost("/estimates",
        async (
            //[FromHeader(Name = "X-Tenant-Id")] string tenantId,
            CreateEstimateRequest request,
            [FromServices] ITenantDbContext db,
            [FromServices] EstimateNumberGenerator generator) =>
        {
            var number = await generator.GenerateAsync();

            var estimate = new Estimate(
                number,
                request.CustomerId,
                DateTime.UtcNow,
                request.ExpiryDateUtc,
                request.Notes
            );

            foreach (var item in request.Items)
            {
                estimate.AddItem(new EstimateItem(
                    item.ProductId,
                    item.Description,
                    item.Quantity,
                    item.UnitPrice,
                    item.TaxRate
                ));
            }

            db.Set<Estimate>().Add(estimate);
            await db.SaveChangesAsync();

            return Results.Ok(new
            {
                estimate.Id,
                estimate.EstimateNumber
            });
        });
    }

    // ---------------------------
    // INVOICES (placeholder)
    // ---------------------------
    private static void MapInvoices(RouteGroupBuilder group)
    {
        group.MapPost("/invoices",
        async (
            CreateInvoiceRequest request,
            //[FromHeader(Name = "X-Tenant-Id")] string tenantId,
            [FromServices] ITenantDbContext db,
            [FromServices] InvoiceNumberGenerator generator) =>
        {
            var invoiceNumber = await generator.GenerateAsync();

            var invoice = new Invoice(
                invoiceNumber,
                request.CustomerId,
                DateTime.UtcNow,
                request.DueDateUtc
            );

            foreach (var item in request.Items)
            {
                invoice.AddItem(new InvoiceItem(
                    item.ProductId,
                    item.Description,
                    item.Quantity,
                    item.UnitPrice,
                    item.TaxRate
                ));
            }

            db.Set<Invoice>().Add(invoice);
            await db.SaveChangesAsync();

            return Results.Ok(new
            {
                invoice.Id,
                invoice.InvoiceNumber
            });
        });
        // existing estimate conversion stays unchanged
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
            //[FromHeader(Name = "X-Tenant-Id")] string tenantId,
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
            //[FromHeader(Name = "X-Tenant-Id")] string tenantId,
            CreateProductRequest request,
            [FromServices] ITenantDbContext db) =>
        {

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