using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Core.Abstractions.MultiTenancy;
public interface ITenantContextualizer
{
    Task SetTenantContextAsync(
        DbContext dbContext,
        CancellationToken ct = default);
    Task SetProvisioningTenantContextAsync(
        DbContext dbContext,
        string schema,
        CancellationToken ct = default);
}