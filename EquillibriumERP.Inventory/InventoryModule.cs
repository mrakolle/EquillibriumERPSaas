using System.Data;
using System.Threading;
using EquillibriumERP.Core.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Inventory;

public class InventoryModule : IModule
{
    public string Name => "Inventory";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        /* services.AddDbContext<InventoryDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("TenantDatabase")));*/
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
    }

    public void MapEndpoints(WebApplication app)
    {
    }

    public async Task MigrateAsync(
        IServiceProvider services,
        string schema,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var db = services.GetRequiredService<InventoryDbContext>();

        var connection = db.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await db.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\", public",
            cancellationToken);

        await db.Database.MigrateAsync(cancellationToken);
    }
}