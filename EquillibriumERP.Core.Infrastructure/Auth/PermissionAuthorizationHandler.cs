using Microsoft.AspNetCore.Authorization;
using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Core.Abstractions.Authorization;
using EquillibriumERP.Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EquillibriumERP.Core.Infrastructure.Authorization;

public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionService _permissionService;

    public PermissionAuthorizationHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            return Task.CompletedTask;

        var hasPermission = _permissionService.HasPermission(userId, requirement.Permission);

        if (hasPermission)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}