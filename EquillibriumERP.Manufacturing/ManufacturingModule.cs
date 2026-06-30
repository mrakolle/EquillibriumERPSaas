using System.Data;
using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.Modules;
using Microsoft.AspNetCore.Routing;
using EquillibriumERP.Manufacturing.Interfaces;
using EquillibriumERP.Manufacturing.Endpoints;
using EquillibriumERP.Manufacturing.Services;
using EquillibriumERP.Manufacturing.Infrastructure.Persistence;
using System.Threading;
using Microsoft.AspNetCore.Http;
using EquillibriumERP.Core.Abstractions;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Products;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace EquillibriumERP.Manufacturing;

public class ManufacturingModule : IModule
{
    public string Name => "Manufacturing";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ManufacturingDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("TenantDatabase")));

        services.AddScoped<IBomService, BomService>();
        //services.AddScoped<IMaterialConsumptionService, MaterialConsumptionService>();
        services.AddScoped<IWorkOrderService, WorkOrderService>();
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
        // EF handled globally
    }

    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/manufacturing")
            .WithTags("Manufacturing")
            .RequireAuthorization();

        BillOfMaterialsEndpoints
            .MapBillOfMaterialsEndpoints(group);

        WorkOrderEndpoints
            .MapWorkOrderEndpoints(group);

        //MapBatches(group);
        //MapBillOfMaterials(group);
        //MapBatchExecution(group);
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