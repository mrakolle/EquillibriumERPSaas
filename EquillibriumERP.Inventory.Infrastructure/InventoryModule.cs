using EquillibriumERP.Core.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Inventory.Infrastructure;

public class InventoryModule : IModule
{
    public string Name => "Inventory";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
    }

    public void MapEndpoints(WebApplication app)
    {
    }
}