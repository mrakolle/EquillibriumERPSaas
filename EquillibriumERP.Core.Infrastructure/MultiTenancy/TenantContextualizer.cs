using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Infrastructure.MultiTenancy;
public sealed class TenantContextualizer : ITenantContextualizer
{
    private readonly ITenantSession _tenantSession;

    public TenantContextualizer(
        ITenantSession tenantSession)
    {
        _tenantSession = tenantSession;
    }

    public async Task SetTenantContextAsync(
        DbContext dbContext,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(_tenantSession.Schema))
            throw new InvalidOperationException(
                "Tenant schema has not been set.");

        await dbContext.Database.OpenConnectionAsync(ct);

        await dbContext.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{_tenantSession.Schema}\", public",
            ct);
    }
    public async Task SetProvisioningTenantContextAsync(
    DbContext dbContext,
    string schema,
    CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(schema))
            throw new ArgumentException(
                "Provisioning schema cannot be null or empty.",
                nameof(schema));

        await dbContext.Database.OpenConnectionAsync(ct);

        await dbContext.Database.ExecuteSqlRawAsync(
            $"SET search_path TO \"{schema}\", public",
            ct);
    }
}
