using System;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public class DesignTimeTenantSession : ITenantSession
{
    public Guid? TenantId { get; set; } = Guid.Empty;
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