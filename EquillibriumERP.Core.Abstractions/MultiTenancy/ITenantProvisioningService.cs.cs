namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface ITenantProvisioningService
{
    Task<string> CreateTenantSchemaAsync(
        Guid tenantId,
        string schema,
        CancellationToken cancellationToken = default);
}

/*public interface ITenantProvisioningService
{
     Task<string> CreateTenantSchemaAsync(Guid tenantId);
}*/