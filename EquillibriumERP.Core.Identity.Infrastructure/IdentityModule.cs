
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Identity.Infrastructure.Services;

namespace EquillibriumERP.Core.Identity.Infrastructure;

public class IdentityModule : IModule
{
    public string Name => "Identity";

    public void Register(IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
    }

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        // intentionally empty for now
    }

    public void MapEndpoints(WebApplication app)
    {
        // no endpoints yet
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
        // identity model mapping later
    }
}