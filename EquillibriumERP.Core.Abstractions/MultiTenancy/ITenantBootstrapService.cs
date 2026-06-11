namespace EquillibriumERP.Core.Abstractions.MultiTenancy;
public interface ITenantBootstrapService
{
    Task InitializeAsync(Guid tenantId, string schema, CancellationToken cancellationToken = default);
}