using EquillibriumERP.Core.Abstractions.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.IoT;

public class IoTDbContext : DbContext
{
    private readonly ITenantSession _tenantSession;

    public IoTDbContext(
        DbContextOptions<IoTDbContext> options,
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
            typeof(IoTDbContext).Assembly);
    }
}