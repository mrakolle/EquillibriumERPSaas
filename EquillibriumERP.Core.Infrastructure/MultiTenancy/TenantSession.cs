using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Infrastructure.MultiTenancy;

public class TenantSession : ITenantSession
{
    public Guid? TenantId { get; set; }
}