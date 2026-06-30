using System.Data;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Abstractions;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Products;
using EquillibriumERP.Products.Auth;
using EquillibriumERP.Products.Interfaces;
using EquillibriumERP.Products.Infrastructure.Endpoints;
using EquillibriumERP.Products.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Products.Endpoints;

namespace EquillibriumERP.Products;

public class ProductsModule : IModule
{
    public string Name => "Products";

    public void RegisterServices(
        IServiceCollection services,
        IConfiguration config)
    {
        
        services.AddDbContext<ProductsDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("TenantDatabase")));
        services.AddScoped<IProductService, ProductService>();
        services.AddSingleton<IModulePermissionProvider,ProductsPermissionProvider>();
        services.AddScoped<IProductLookup, ProductLookupService>();
        services.AddScoped<IRawMaterialSeeder, RawMaterialSeederService>();
    }

    public void RegisterModel(
        Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
    {
    }

    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/products")
            .WithTags("Products")
            .RequireAuthorization();
        ProductProvisioningEndpoints
            .MapProductProvisioningEndpoints(group);
        SeedDataEndpoints
            .MapSeedDataEndpoints(group);
    }
    public async Task MigrateAsync(
        IServiceProvider services,
        string schema,
        CancellationToken cancellationToken)
    {
        var db = services.GetRequiredService<ProductsDbContext>();

        await db.Database.GetDbConnection().OpenAsync(cancellationToken);

        await db.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\", public",
            cancellationToken);

        await db.Database.MigrateAsync(cancellationToken);
    }
}