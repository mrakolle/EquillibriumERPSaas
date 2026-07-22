namespace EquillibriumERP.Core.Abstractions.Identity;

public interface ITenantAdminUserService
{
    Task CreateTenantAdminAsync(
        CreateTenantRequest request,
        Guid tenantId,
        string schema,
        CancellationToken ct = default);
}