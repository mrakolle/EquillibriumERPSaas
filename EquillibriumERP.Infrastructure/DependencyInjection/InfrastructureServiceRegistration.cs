using EquillibriumERP.Infrastructure.Persistence;
using EquillibriumERP.Infrastructure.MultiTenancy;
using EquillibriumERP.Infrastructure.Modules;
using EquillibriumERP.Abstractions.MultiTenancy;
using EquillibriumERP.Abstractions.Modules;
using EquillibriumERP.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EquillibriumERP.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // =====================================================
        // MODULE DISCOVERY (CRITICAL FOR EF CORE)
        // =====================================================
        services.AddSingleton<IModuleAssemblyProvider, ModuleAssemblyProvider>();

        // =====================================================
        // MASTER DB
        // =====================================================
        services.AddDbContext<MasterDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("MasterDatabase"));
        });

        // =====================================================
        // MULTI-TENANCY CORE SERVICES
        // =====================================================
        services.AddScoped<ITenantResolver, TenantResolver>();
        services.AddScoped<TenantProvisioningService>();
        services.AddScoped<TenantSchemaMigrator>();

        // =====================================================
        // TENANT DB (MODULE-DRIVEN EF CORE)
        // =====================================================
        services.AddScoped<TenantDbContext>(provider =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var moduleProvider = provider.GetRequiredService<IModuleAssemblyProvider>();

            var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();

            optionsBuilder.UseNpgsql(
                config.GetConnectionString("TenantDatabase"));

            optionsBuilder.ReplaceService<IModelCacheKeyFactory, TenantModelCacheKeyFactory>();

            var tenantResolver = provider.GetRequiredService<ITenantResolver>();

            return new TenantDbContext(
                optionsBuilder.Options,
                moduleProvider,
                tenantResolver);
        });
        services.AddScoped<ITenantDbContext>(provider =>
            provider.GetRequiredService<TenantDbContext>());

        return services;
    }
}