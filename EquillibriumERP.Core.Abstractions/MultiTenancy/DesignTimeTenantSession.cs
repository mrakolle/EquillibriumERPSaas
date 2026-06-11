using System;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public class DesignTimeTenantSession : ITenantSession
{
    public Guid? TenantId { get; set; } = Guid.Empty;
}