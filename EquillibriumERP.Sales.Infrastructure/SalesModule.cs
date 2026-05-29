using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Sales.Domain.Entities;
using EquillibriumERP.Sales.Domain.Enums;

using EquillibriumERP.Sales.Infrastructure.Services;
using EquillibriumERP.Sales.Application.Features.Invoices;
//using EquillibriumERP.Sales.Infrastructure.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace EquillibriumERP.Sales.Infrastructure;

public class SalesModule : IModule
{
    public string Name => "Sales";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        Console.WriteLine("REGISTERING SALES MODULE SERVICES");

        services.AddScoped<InvoiceNumberGenerator>();
        services.AddScoped<EstimateNumberGenerator>();
        //services.AddScoped<IProductService, ProductService>();
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
        // EF handled centrally (tenant DbContext)
    }

    public void MapEndpoints(WebApplication app)
    {
       /* var group = app.MapGroup("/sales");

        MapProducts(group);
        MapEstimates(group);
        MapInvoices(group);*/
        return;
    }

    // ---------------------------
    // ESTIMATES
    // ---------------------------
    private static void MapEstimates(RouteGroupBuilder group)
    {
        group.MapGet("/estimates", async (
            ITenantDbContext db) =>
        {
            var estimates = await db.Set<Estimate>()
                .AsNoTracking()
                .OrderByDescending(x => x.EstimateDateUtc)
                .ToListAsync();

            return Results.Ok(estimates);
        });

        group.MapGet("/estimates/{id:guid}", async (
            Guid id,
            ITenantDbContext db) =>
        {
            var estimate = await db.Set<Estimate>()
                .Include(x => x.Items)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return estimate is null
                ? Results.NotFound()
                : Results.Ok(estimate);
        });
    }

    // ---------------------------
    // INVOICES
    // ---------------------------
    private static void MapInvoices(RouteGroupBuilder group)
    {
        group.MapPost("/invoices", async (
            CreateInvoiceRequest request,
            ITenantDbContext db,
            InvoiceNumberGenerator generator) =>
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
    }
}
