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
using EquillibriumERP.Core.Onboarding.Services;
using EquillibriumERP.Core.Onboarding.Persistence;
using EquillibriumERP.Core.Infrastructure.MultiTenancy;
using EquillibriumERP.Core.Onboarding.Endpoints;

namespace EquillibriumERP.Core.Onboarding;

public sealed class OnboardingModule : IModule
{
    public string Name => "Onboarding";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITenantOnboardingService, TenantOnboardingService>();
        services.AddScoped<ITenantProvisioningService, TenantProvisioningService>();
        services.AddDbContext<OnboardingDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("TenantDatabase")));

        services.AddScoped<ITenantOnboardingService, TenantOnboardingService>();
    }

    public void RegisterModel(ModelBuilder modelBuilder) { }

    public void MapEndpoints(WebApplication app)
    {
        OnboardingEndpoints.MapEndpoints(app);
    }

    public async Task MigrateAsync(
        IServiceProvider services,
        string schema,
        CancellationToken cancellationToken)
    {
        var db = services.GetRequiredService<OnboardingDbContext>();

        var connection = db.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await db.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"public\"", // onboarding ALWAYS public
            cancellationToken);

        await db.Database.MigrateAsync(cancellationToken);
    }
}