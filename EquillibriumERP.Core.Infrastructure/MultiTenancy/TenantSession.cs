using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Infrastructure.MultiTenancy;

public class TenantSession : ITenantSession
{
    public Guid? TenantId { get; set; }
    public string? Schema { get; private set; }

    public void SetTenant(Guid tenantId, string schema)
    {
        TenantId = tenantId;
        Schema = schema;
    }

    public void Clear()
    {
        TenantId = null;
        Schema = null;
    }
}