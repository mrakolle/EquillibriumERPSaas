using EquillibriumERP.Abstractions.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Manufacturing.Infrastructure;

public class ManufacturingModule : IModule
{
    public string Name => "Manufacturing";

    public void RegisterServices(IServiceCollection services)
    {
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
    }
}
