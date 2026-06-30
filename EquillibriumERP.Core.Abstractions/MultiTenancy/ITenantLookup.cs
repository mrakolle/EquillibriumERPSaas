namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public interface ITenantLookup
{
    Task<TenantInfo?> GetByCodeAsync(string code, CancellationToken ct = default);
}

