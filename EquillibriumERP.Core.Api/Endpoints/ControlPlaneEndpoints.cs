using Microsoft.AspNetCore.Mvc;
using EquillibriumERP.ControlPlane.Onboarding;

namespace EquillibriumERP.Core.Api.Endpoints;

public static class ControlPlaneEndpoints
{
    public static IEndpointRouteBuilder MapControlPlaneEndpoints(
        this IEndpointRouteBuilder app)
    {
        Console.WriteLine("CONTROL PLANE ONBOARD HIT 1");
        app.MapPost(
            "/control-plane/onboard",
            async (
                string tenantName,
                ITenantOnboardingService onboardingService,
                CancellationToken cancellationToken) =>
            {
                Console.WriteLine("CONTROL PLANE ONBOARD HIT 2");
                var tenantId = await onboardingService
                    .OnboardTenantAsync(
                        tenantName,
                        cancellationToken);

                return Results.Ok(new
                {
                    TenantId = tenantId
                });
            });

        return app;
    }
}