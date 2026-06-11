using System.Data;
using EquillibriumERP.Core.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Manufacturing.Application.Features.Batches;
using EquillibriumERP.Manufacturing.Application.Features.BillOfMaterials;
using EquillibriumERP.Manufacturing.Domain.Services;
using EquillibriumERP.Manufacturing.Persistence;

namespace EquillibriumERP.Manufacturing;

public class ManufacturingModule : IModule
{
    public string Name => "Manufacturing";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        /*services.AddDbContext<ManufacturingDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("TenantDatabase")));*/
        // Application services
       // services.AddScoped<BatchService>();
       // services.AddScoped<BillOfMaterialsService>();

        // Domain services
       // services.AddScoped<BatchMovementService>();
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
        // EF handled globally
    }

    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/manufacturing");

        //MapBatches(group);
        //MapBillOfMaterials(group);
        //MapBatchExecution(group);
    }

    private static void MapBatches(RouteGroupBuilder group)
    {
        group.MapGroup("/batches");
    }

    private static void MapBillOfMaterials(RouteGroupBuilder group)
    {
        group.MapGroup("/bill-of-materials");
    }

    private static void MapBatchExecution(RouteGroupBuilder group)
    {
        group.MapGroup("/batch-execution");
    }

    public async Task MigrateAsync(
    IServiceProvider services,
    string schema,
    CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var db = services.GetRequiredService<ManufacturingDbContext>();

        var connection = db.Database.GetDbConnection();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync(cancellationToken);

        await db.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\", public",
            cancellationToken);

        await db.Database.MigrateAsync(cancellationToken);
    }
}