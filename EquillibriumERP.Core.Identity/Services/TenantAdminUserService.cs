using EquillibriumERP.Core.Abstractions.Identity;
using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Core.Identity.Services;

public sealed class TenantAdminUserService : ITenantAdminUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public TenantAdminUserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task CreateTenantAdminAsync(
    Guid tenantId,
    string tenantCode,
    string schema,
    CancellationToken ct = default)
    {
        var exists = await _userManager.Users
            .AnyAsync(x => x.TenantId == tenantId, ct);

        if (exists)
            return;

        var admin = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            TenantId = tenantId,
            Email = $"admin@{tenantCode}.local",
            UserName = $"admin@{tenantCode}.local",
            NormalizedEmail = $"ADMIN@{tenantCode}.LOCAL",
            NormalizedUserName = $"ADMIN@{tenantCode}.LOCAL",
            FirstName = "System",
            LastName = "Admin",
            IsActive = true
        };

        var result = await _userManager.CreateAsync(admin, "Admin@123");

        if (!result.Succeeded)
            throw new InvalidOperationException(
                string.Join(", ", result.Errors.Select(e => e.Description)));
    }
}