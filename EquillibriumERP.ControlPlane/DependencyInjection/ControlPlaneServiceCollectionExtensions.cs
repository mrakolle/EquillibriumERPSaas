using EquillibriumERP.ControlPlane.Onboarding;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.ControlPlane.DependencyInjection;

public static class ControlPlaneServiceCollectionExtensions
{
    public static IServiceCollection AddControlPlane(
        this IServiceCollection services)
    {
        services.AddScoped<
            ITenantOnboardingService,
            TenantOnboardingService>();

        return services;
    }
}