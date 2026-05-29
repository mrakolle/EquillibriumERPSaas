using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Products.Application.Interfaces;
using EquillibriumERP.Products.Infrastructure.Endpoints;
using EquillibriumERP.Products.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Products.Infrastructure;

public class ProductsModule : IModule
{
    public string Name => "Products";

    public void RegisterServices(
        IServiceCollection services,
        IConfiguration config)
    {
        services.AddScoped<IProductService, ProductService>();
    }

    public void RegisterModel(
        Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
    {
    }

    public void MapEndpoints(WebApplication app)
    {
        ProductProvisioningEndpoints
            .MapProductProvisioningEndpoints(app);
    }
}