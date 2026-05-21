using EquillibriumERP.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace EquillibriumERP.Infrastructure.Modules;

public class ModuleExecutor : IModuleExecutor
{
    private readonly IEnumerable<IModule> _modules;

    // ✅ DI will inject all IModule implementations automatically
    public ModuleExecutor(IEnumerable<IModule> modules)
    {
        _modules = modules;
    }

    public void RegisterModules(IServiceCollection services, IConfiguration config)
    {
        foreach (var module in _modules)
        {
            module.RegisterServices(services, config);
        }
    }

    public void MapModules(WebApplication app)
    {
        foreach (var module in _modules)
        {
            module.MapEndpoints(app);
        }
    }
}