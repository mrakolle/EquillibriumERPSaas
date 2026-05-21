using EquillibriumERP.Abstractions.MultiTenancy;

namespace EquillibriumERP.Infrastructure.MultiTenancy;

public class TenantSession : ITenantSession
{
    public Guid? TenantId { get; set; }
}