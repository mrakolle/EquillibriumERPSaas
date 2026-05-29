using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Core.Abstractions.Modules;

public interface IModuleExecutor
{
    void RegisterModules(IServiceCollection services, IConfiguration config);
    void MapModules(WebApplication app);
}