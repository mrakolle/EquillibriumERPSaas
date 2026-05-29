namespace EquillibriumERP.Core.Abstractions.MultiTenancy;
public interface IUserContext
{
    Guid UserId { get; }
    Guid TenantId { get; }
    string Email { get; }
}