using System.Data;
using Microsoft.AspNetCore.Builder;
using EquillibriumERP.Core.Abstractions.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Sales;

public class SalesModule : IModule
{
    public string Name => "Sales";

    public void RegisterServices(
        IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<SalesDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("TenantDatabase")));
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
        // intentionally empty (same as Identity + Products)
    }

    public void MapEndpoints(WebApplication app)
    {
        
    }
    
    public async Task MigrateAsync(
        IServiceProvider services,
        string schema,
        CancellationToken cancellationToken)
    {
        var db = services.GetRequiredService<SalesDbContext>();

        await db.Database.GetDbConnection().OpenAsync(cancellationToken);

        await db.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\", public",
            cancellationToken);

        await db.Database.MigrateAsync(cancellationToken);
    }
}