using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using EquillibriumERP.Core.Infrastructure.Authorization;

namespace EquillibriumERP.Core.Infrastructure.Authorization;

public static class AuthorizationSetup
{
    public static IServiceCollection AddPermissionAuthorization(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

        return services;
    }
}