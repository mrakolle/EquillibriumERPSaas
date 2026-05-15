using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Abstractions.Modules;

public interface IModule
{
    string Name { get; }

    void RegisterServices(IServiceCollection services);

    void RegisterModel(ModelBuilder modelBuilder);
}