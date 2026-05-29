namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class BillingProfile
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string CompanyName { get; set; } = string.Empty;

    public string TaxNumber { get; set; } = string.Empty;

    public string BillingEmail { get; set; } = string.Empty;
}