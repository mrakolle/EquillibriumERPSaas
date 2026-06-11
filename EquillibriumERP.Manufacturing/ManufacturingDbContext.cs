using EquillibriumERP.Core.Abstractions.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Manufacturing;

public class ManufacturingDbContext : DbContext
{
    private readonly ITenantSession _tenantSession;

    public ManufacturingDbContext(
        DbContextOptions<ManufacturingDbContext> options,
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
            typeof(ManufacturingDbContext).Assembly);
    }
}