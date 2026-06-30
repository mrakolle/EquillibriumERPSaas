
using EquillibriumERP.Core.Abstractions.MultiTenancy;
namespace EquillibriumERP.Core.Abstractions.MultiTenancy;
public class TenantExecutionContext : ITenantExecutionContext
{
    private readonly ITenantResolver _resolver;

    public TenantExecutionContext(ITenantResolver resolver)
    {
        _resolver = resolver;
    }

    public async Task RunInTenantAsync(
        string tenantCode,
        Func<Task> action,
        CancellationToken ct = default)
    {
        _resolver.SetTenant(tenantCode);

        await action();
    }
}