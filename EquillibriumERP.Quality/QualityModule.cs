using System.Threading;
using EquillibriumERP.Core.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace EquillibriumERP.Quality;

public class QualityModule : IModule
{
    public string Name => "Quality";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<QualityDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("TenantDatabase")));
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

        var db = services.GetRequiredService<QualityDbContext>();

        await db.Database.MigrateAsync(cancellationToken);
    }
}