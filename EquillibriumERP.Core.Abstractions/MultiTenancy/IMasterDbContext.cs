using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface IMasterDbContext
{
    Task UpdateTenantSchemasAsync(CancellationToken ct);
    Task<List<Abstractions.MultiTenancy.Tenant>> GetActiveTenantsAsync(CancellationToken ct);

}