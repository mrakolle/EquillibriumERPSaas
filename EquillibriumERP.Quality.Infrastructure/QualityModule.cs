using EquillibriumERP.Abstractions.Modules;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Quality.Infrastructure;

public class QualityModule : IModule
{
    public string Name => "Quality";

    public void RegisterServices(IServiceCollection services)
    {
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
    }
}
