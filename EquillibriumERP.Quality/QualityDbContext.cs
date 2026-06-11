using EquillibriumERP.Core.Abstractions.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Quality;

public class QualityDbContext : DbContext
{
    private readonly ITenantSession _tenantSession;

    public QualityDbContext(
        DbContextOptions<QualityDbContext> options,
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
            typeof(QualityDbContext).Assembly);
    }
}