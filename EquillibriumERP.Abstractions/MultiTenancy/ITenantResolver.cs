namespace EquillibriumERP.Abstractions.MultiTenancy;

public interface ITenantResolver
{
    void SetTenant(string tenantId);
    string GetSchema();
    string GetTenantId();
}