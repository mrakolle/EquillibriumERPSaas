using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Identity.Domain.Entities;
using EquillibriumERP.Core.Identity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EquillibriumERP.Core.Identity;

public sealed class IdentitySeeder : ITenantModuleSeeder
{
    private readonly IdentityDbContext _db;
     public string Name => "Identity";
     public int Order => 100;

    public IdentitySeeder(IdentityDbContext db)
    {
        _db = db;
    }

    

    public async Task SeedAsync(
    string? schema = null,
    CancellationToken ct = default)
    {
        await SeedRolesAsync(ct);
        await SeedAdminUserAsync(ct);
    }

   private async Task SeedAdminUserAsync(CancellationToken ct)
    {
        // 🔥 deterministic guard
        var hasAnyUsers = await _db.Users.AnyAsync(ct);

        if (hasAnyUsers)
            return;

        // 🔥 hard-coded role ID to avoid lookup timing/schema issues
        var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var adminUser = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@tenant.local",
            NormalizedEmail = "ADMIN@TENANT.LOCAL",
            EmailConfirmed = true,
            IsActive = true,
            TenantId = Guid.Empty
        };

        var hasher = new PasswordHasher<ApplicationUser>();
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

        _db.Users.Add(adminUser);

        _db.UserRoles.Add(new IdentityUserRole<Guid>
        {
            UserId = adminUser.Id,
            RoleId = adminRoleId
        });

        await _db.SaveChangesAsync(ct);
    }
    private async Task SeedRolesAsync(CancellationToken ct)
    {
        var hasAnyRoles = await _db.Roles.AnyAsync(ct);

        if (hasAnyRoles)
            return;

        _db.Roles.Add(new ApplicationRole
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "Admin",
            NormalizedName = "ADMIN"
        });

        await _db.SaveChangesAsync(ct);
    }

    private async Task EnsureRoleAsync(
        string roleName,
        string description,
        CancellationToken ct)
    {
        var role = await _db.Roles
            .FirstOrDefaultAsync(r => r.Name == roleName, ct);

        if (role != null)
            return;

        _db.Roles.Add(new ApplicationRole
        {
            Id = Guid.NewGuid(),
            Name = roleName,
            NormalizedName = roleName.ToUpperInvariant(),
            Description = description
        });

        await _db.SaveChangesAsync(ct);
    }
}