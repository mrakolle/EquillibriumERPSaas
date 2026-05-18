using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Abstractions.Modules;

public interface IModule
{
    string Name { get; }

    void RegisterServices(IServiceCollection services, IConfiguration config);

    void RegisterModel(ModelBuilder modelBuilder);

    void MapEndpoints(WebApplication app);
}