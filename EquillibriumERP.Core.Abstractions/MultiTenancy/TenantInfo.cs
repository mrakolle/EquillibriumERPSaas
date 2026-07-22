namespace EquillibriumERP.Core.Abstractions.MultiTenancy;
public sealed class TenantInfo
{
    public Guid Id { get; init; }

    public string Code { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string Schema { get; init; } = string.Empty;
}