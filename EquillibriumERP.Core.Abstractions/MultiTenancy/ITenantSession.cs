namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface ITenantSession
{
    Guid? TenantId { get; set; }
}