using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Infrastructure.MultiTenancy;

public class TenantSeederRunner
{
    private readonly IEnumerable<ITenantModuleSeeder> _seeders;
    private readonly ITenantExecutionContext _context;

    public TenantSeederRunner(
        IEnumerable<ITenantModuleSeeder> seeders,
        ITenantExecutionContext context)
    {
        _seeders = seeders;
        _context = context;
    }

    public async Task SeedTenantAsync(string tenantCode, CancellationToken ct)
    {
        await _context.RunInTenantAsync(tenantCode, async () =>
        {
            foreach (var seeder in _seeders)
            {
                await seeder.SeedAsync(
                        schema: null,
                        ct: ct);
            }
        }, ct);
    }
}