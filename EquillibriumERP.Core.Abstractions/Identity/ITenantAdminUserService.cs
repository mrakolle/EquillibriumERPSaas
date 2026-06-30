namespace EquillibriumERP.Core.Abstractions.Identity;

public interface ITenantAdminUserService
{
    Task CreateTenantAdminAsync(
    Guid tenantId,
    string tenantCode,
    string schema,
    CancellationToken ct = default);
}