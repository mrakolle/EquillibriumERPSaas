using EquillibriumERP.Abstractions.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Sales.Infrastructure;

public class SalesModule : IModule
{
    public string Name => "Sales";

    public void RegisterServices(IServiceCollection services)
    {
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
    }
}
