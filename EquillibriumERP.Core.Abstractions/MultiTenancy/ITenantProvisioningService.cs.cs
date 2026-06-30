namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface ITenantProvisioningService
{
    Task<string> CreateTenantSchemaAsync(
        Guid tenantId,
        string schema,
        CancellationToken cancellationToken = default);

    Task UpdateTenantSchemaAsync(
        string schema,
        CancellationToken cancellationToken);

    
}

/*public interface ITenantProvisioningService
{
     Task<string> CreateTenantSchemaAsync(Guid tenantId);
}*/