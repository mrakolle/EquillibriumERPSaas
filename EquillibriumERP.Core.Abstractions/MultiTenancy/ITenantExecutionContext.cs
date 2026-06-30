
namespace EquillibriumERP.Core.Abstractions.MultiTenancy;
public interface ITenantExecutionContext
{
    Task RunInTenantAsync(string tenantCode, Func<Task> action, CancellationToken ct = default);
}