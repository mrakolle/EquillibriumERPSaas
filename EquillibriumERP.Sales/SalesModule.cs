using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Sales.Infrastructure.Persistence;
using EquillibriumERP.Sales.Interfaces;
using EquillibriumERP.Sales.Services;
using EquillibriumERP.Core.Abstractions;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.Sales.Endpoints;


namespace EquillibriumERP.Sales;

public class SalesModule : IModule
{
    public string Name => "Sales";

    public void RegisterServices(
        IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContext<SalesDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("TenantDatabase")));

        services.AddScoped<IEstimateService, EstimateService>();
        services.AddScoped<ICustomerService, CustomerService>();
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
        // intentionally empty (same as Identity + Products)
    }

    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/sales")
            .WithTags("Sales")
            .RequireAuthorization();

        EstimatesEndpoints
            .MapEstimateEndpoints(group);

        CustomersEndpoints
            .MapCustomersEndpoints(group);
    }
    
    public async Task MigrateAsync(
        IServiceProvider services,
        string schema,
        CancellationToken cancellationToken)
    {
        var db = services.GetRequiredService<SalesDbContext>();

        await db.Database.GetDbConnection().OpenAsync(cancellationToken);

        await db.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\", public",
            cancellationToken);

        await db.Database.MigrateAsync(cancellationToken);
    }
}