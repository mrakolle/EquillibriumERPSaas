using System.Data;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Abstractions;
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
        services.AddDbContext<ProductsDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("TenantDatabase")));
        services.AddScoped<IProductService, ProductService>();
        services.AddSingleton<IModulePermissionProvider,ProductsPermissionProvider>();
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