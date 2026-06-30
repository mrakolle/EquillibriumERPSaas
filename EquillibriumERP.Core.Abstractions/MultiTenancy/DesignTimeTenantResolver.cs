
namespace EquillibriumERP.Core.Abstractions.MultiTenancy;

public class DesignTimeTenantResolver : ITenantResolver
{
    private string? _tenantId;

    public void SetTenant(string tenantId)
    {
        _tenantId = tenantId;
    }

    public string GetTenantId()
    {
        return _tenantId ?? string.Empty;
    }

    public string GetSchema()
    {
        return _tenantId is null
            ? "public"
            : $"tenant_{_tenantId:N}";
    }

    public string GetSchema(string tenantCode)
    {
        throw new NotImplementedException();
    }
}