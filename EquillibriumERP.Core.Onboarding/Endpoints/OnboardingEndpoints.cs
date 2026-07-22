using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Onboarding.Endpoints;

public static class OnboardingEndpoints
{
    public static void MapEndpoints(WebApplication app)
    {
        var group = app.MapGroup("/onboarding");

        group.MapPost("/tenants", async (
            CreateTenantRequest request,
            ITenantOnboardingService service,
            CancellationToken ct) =>
        {
            var result = await service.OnboardTenantAsync(request, ct);

            return Results.Ok(result);
        });
    }
}