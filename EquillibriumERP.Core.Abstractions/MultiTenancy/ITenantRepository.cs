namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface ITenantRepository
{
    Task<Guid> CreateTenantAsync(
        string tenantName,
        string schema,
        CancellationToken cancellationToken);
}