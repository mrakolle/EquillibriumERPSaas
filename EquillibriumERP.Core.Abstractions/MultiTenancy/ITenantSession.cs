namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface ITenantSession
{
    Guid? TenantId { get; }
    string? Schema { get; }

    void SetTenant(Guid tenantId, string schema);
    void Clear();
}