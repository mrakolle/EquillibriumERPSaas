namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class TenantFeature
{
    public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; } = null!;

    public Guid FeatureId { get; set; }

    public Feature Feature { get; set; } = null!;

    public bool IsEnabled { get; set; }
}