namespace EquillibriumERP.ControlPlane.Endpoints;

public sealed class CreateTenantRequest
{
    public string TenantName { get; set; } = default!;
}