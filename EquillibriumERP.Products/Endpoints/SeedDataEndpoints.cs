using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Products.Endpoints;

public static class SeedDataEndpoints
{
    public static void MapSeedDataEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/seed-data/raw-materials",
            async (
                IRawMaterialSeeder seeder,
                CancellationToken ct) =>
            {
                await seeder.SeedAsync(null, ct);

                return Results.Ok(new
                {
                    message = "Raw materials seeding completed"
                });
            })
            .WithTags("Seed Data")
            .AllowAnonymous(); // or Authorize if needed
    }
}