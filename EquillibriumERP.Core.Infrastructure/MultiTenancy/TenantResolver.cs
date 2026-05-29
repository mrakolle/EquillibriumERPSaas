
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Core.Infrastructure.MultiTenancy;

public class TenantResolver : ITenantResolver
{
    private string? _tenantId;

    public void SetTenant(string tenantId)
    {
        if (string.IsNullOrWhiteSpace(tenantId))
            throw new InvalidOperationException("TenantId cannot be empty");

        _tenantId = tenantId;
    }

    public string GetTenantId()
    {
        return _tenantId ?? "default";
    }

    public string GetSchema()
    {
        var tenantId = GetTenantId();

        if (string.IsNullOrWhiteSpace(tenantId) || tenantId == "default")
            return "public";   // SAFE fallback for EF model creation

        return $"tenant_{tenantId.Replace("-", "")}";
    }

}