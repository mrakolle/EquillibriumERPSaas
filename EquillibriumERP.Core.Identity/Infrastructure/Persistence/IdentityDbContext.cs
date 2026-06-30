using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Core.Identity.Infrastructure;

public class IdentityDbContext
    : IdentityDbContext<
        ApplicationUser,
        ApplicationRole,
        Guid,
        IdentityUserClaim<Guid>,
        IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>
{
    private readonly ITenantSession _tenantSession;
    

    public IdentityDbContext(
        DbContextOptions<IdentityDbContext> options,
        ITenantSession tenantSession)
        : base(options)
    {
        _tenantSession = tenantSession;
    }

    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<UserTenant> UserTenants => Set<UserTenant>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }

    /*protected override void OnModelCreating(ModelBuilder builder)
    {
        var schema = _tenantSession?.TenantId == Guid.Empty
            ? "public"
            : $"tenant_{_tenantSession.TenantId:N}";

        builder.HasDefaultSchema(schema);

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }*/
}