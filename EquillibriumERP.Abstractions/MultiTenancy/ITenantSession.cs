namespace EquillibriumERP.Abstractions.MultiTenancy;

public interface ITenantSession
{
    Guid? TenantId { get; set; }
}