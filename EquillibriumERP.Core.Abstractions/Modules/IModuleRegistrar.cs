using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Core.Abstractions.Modules;

public interface IModuleRegistrar
{
    void RegisterModules(IServiceCollection services, IConfiguration configuration);

    void MapEndpoints(WebApplication app);
}