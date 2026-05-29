namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface ITenantResolver
{
    void SetTenant(string tenantId);

    string GetTenantId();

    string GetSchema();
}