namespace EquillibriumERP.ControlPlane.Tenancy;

public sealed class TenantInfo
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string Schema { get; init; } = default!;

    public bool IsActive { get; init; } = true;
}