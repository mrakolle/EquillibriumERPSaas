using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Infrastructure.Modules;

namespace EquillibriumERP.Core.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IModuleAssemblyProvider, ModuleAssemblyProvider>();

        return services;
    }
}