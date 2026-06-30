using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Infrastructure.MultiTenancy;
using EquillibriumERP.ControlPlane.Endpoints;
using EquillibriumERP.ControlPlane.Services;
using EquillibriumERP.ControlPlane.Infrastructure.Persistence;
using EquillibriumERP.ControlPlane.Interfaces;


namespace EquillibriumERP.Core.Onboarding;

public sealed class ControlPlaneModule : IModule
{
    public string Name => "ControlPlane";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<
            ITenantMigrationService,TenantMigrationService>();

        services.AddScoped<ITenantProvisioningService, TenantProvisioningService>();
        services.AddScoped<ITenantMigrationService, TenantMigrationService>();
        services.AddDbContext<ControlPlaneDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("TenantDatabase")));
    }

    public void RegisterModel(ModelBuilder modelBuilder) { }

    public void MapEndpoints(WebApplication app)
    {
        TenantAdminEndpoints
            .MapTenantAdminEndpoints(app);
    }

    public async Task MigrateAsync(
        IServiceProvider services,
        string schema,
        CancellationToken cancellationToken)
    {
       /* var db = services.GetRequiredService<ControlPlaneDbContext>();

        var connection = db.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await db.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"public\"", // onboarding ALWAYS public
            cancellationToken);

        await db.Database.MigrateAsync(cancellationToken);*/
    }
}