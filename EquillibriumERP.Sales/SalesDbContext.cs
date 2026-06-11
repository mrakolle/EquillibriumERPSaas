using EquillibriumERP.Core.Abstractions.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Sales;

public class SalesDbContext : DbContext
{
    private readonly ITenantSession _tenantSession;

    public SalesDbContext(
        DbContextOptions<SalesDbContext> options,
        ITenantSession tenantSession)
        : base(options)
    {
        _tenantSession = tenantSession;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(
            $"tenant_{_tenantSession.TenantId:N}");

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
            typeof(SalesDbContext).Assembly);
    }
}