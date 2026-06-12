using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Abstractions.Inventory;
using EquillibriumERP.Inventory.Services;
using EquillibriumERP.Core.Abstractions;
using EquillibriumERP.Inventory.Endpoints;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Inventory;

public class InventoryModule : IModule
{
    public string Name => "Inventory";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<InventoryDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("TenantDatabase")));

        services.AddScoped<IStockMovementService, StockMovementService>();

        services.AddSingleton<IModulePermissionProvider,
            InventoryPermissionProvider>();
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
        // not used (we rely on ApplyConfigurationsFromAssembly)
    }

    public void MapEndpoints(WebApplication app)
    {
        InventoryEndpoints
            .MapInventoryEndpoints(app);
    }

    public async Task MigrateAsync(
        IServiceProvider services,
        string schema,
        CancellationToken cancellationToken)
    {
        var db = services.GetRequiredService<InventoryDbContext>();

        await db.Database.GetDbConnection().OpenAsync(cancellationToken);

        // 🔥 CRITICAL: switch tenant schema
        await db.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\", public",
            cancellationToken);

        await db.Database.MigrateAsync(cancellationToken);
    }
}