using EquillibriumERP.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Sales.Infrastructure;

public class SalesModule : IModule
{
    public string Name => "Sales";

    public void RegisterServices(IServiceCollection services, IConfiguration config)
    {
    }

    public void RegisterModel(ModelBuilder modelBuilder)
    {
    }

    public void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/sales");

        group.MapGet("/health", () => "Sales module alive");

        group.MapGet("/orders", () =>
        {
            return new[]
            {
                new { Id = 1, Code = "SO-001" },
                new { Id = 2, Code = "SO-002" }
            };
        });

        group.MapPost("/orders", (CreateSalesOrderRequest request) =>
        {
            return new
            {
                Id = Random.Shared.Next(1000, 9999),
                request.Code
            };
        });
    }
}

public record CreateSalesOrderRequest(string Code);