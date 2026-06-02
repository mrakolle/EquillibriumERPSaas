using Microsoft.AspNetCore.Authorization;
using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EquillibriumERP.Core.Infrastructure.Authorization;

public sealed class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly MasterDbContext _db;

    public PermissionAuthorizationHandler(MasterDbContext db)
    {
        _db = db;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null)
            return;

        var userId = Guid.Parse(userIdClaim.Value);

        var hasPermission = await _db.MasterUserRoles
            .Where(ur => ur.UserId == userId)
            .SelectMany(ur => ur.Role.RolePermissions)
            .AnyAsync(rp => rp.Permission.Code == requirement.Permission);

        if (hasPermission)
            context.Succeed(requirement);
    }
}