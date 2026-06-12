using EquillibriumERP.Core.Abstractions;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Purchasing.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddSingleton<IModulePermissionProvider,
                PurchasingPermissionProvider>();
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
    }

    public void MapEndpoints(WebApplication app)
    {
        SupplierEndpoints
            .MapSupplierEndpoints(app);

        PurchaseOrderEndpoints
            .MapPurchaseOrderEndpoints(app);

        GoodsReceiptEndpoints
            .MapGoodsReceiptEndpoints(app);
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