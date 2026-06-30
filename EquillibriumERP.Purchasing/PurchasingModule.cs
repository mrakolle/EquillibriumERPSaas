using EquillibriumERP.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Purchasing.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Purchasing.Interfaces;
using EquillibriumERP.Purchasing.Services;
using EquillibriumERP.Purchasing.Auth;
using EquillibriumERP.Purchasing.Infrastructure.Persistence;

namespace EquillibriumERP.Purchasing;

public class PurchasingModule : IModule
{
    public string Name => "Purchasing";

    public void RegisterServices(
        IServiceCollection services,
        IConfiguration config)
    {
        
        services.AddDbContext<PurchasingDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("TenantDatabase")));

        services.AddDbContext<PurchasingDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("TenantDatabase")));

        services.AddSingleton<IModulePermissionProvider,
                PurchasingPermissionProvider>();
                
        services.AddScoped<ISupplierService,SupplierService>();
        services.AddScoped<IPurchaseOrderService,PurchaseOrderService>();
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
    }

    public void MapEndpoints(WebApplication app)
    {
         var group = app.MapGroup("/purchasing")
            .WithTags("Purchasing");

        SupplierEndpoints
            .MapSupplierEndpoints(group);

        PurchaseOrderEndpoints
            .MapPurchaseOrderEndpoints(group);

        GoodsReceiptEndpoints
            .MapGoodsReceiptEndpoints(group);
    }

    public async Task MigrateAsync(
        IServiceProvider services,
        string schema,
        CancellationToken cancellationToken)
    {
        var db = services.GetRequiredService<PurchasingDbContext>();

        await db.Database.GetDbConnection().OpenAsync(cancellationToken);

        await db.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\", public",
            cancellationToken);

        await db.Database.MigrateAsync(cancellationToken);
    }
}