namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class BillingInvoice
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public decimal Amount { get; set; }

    public DateTime InvoiceDateUtc { get; set; }

    public bool IsPaid { get; set; }
}