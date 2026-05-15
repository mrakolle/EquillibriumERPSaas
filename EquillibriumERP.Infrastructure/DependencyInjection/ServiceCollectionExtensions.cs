using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Abstractions.Modules;
using EquillibriumERP.Infrastructure.Modules;

namespace EquillibriumERP.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IModuleAssemblyProvider, ModuleAssemblyProvider>();

        return services;
    }
}