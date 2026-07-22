using EquillibriumERP.Sales.Domain.Enums;

namespace EquillibriumERP.Sales.Domain.Entities;

public class Estimate
{
    public Guid Id { get; set; }

    public string QuoteNumber { get;  set; } = string.Empty;
    public string? Reference { get; set; }

    public Guid CustomerId { get; set; }

    // Snapshot
    public string CustomerName { get; set; } = string.Empty;

    public EstimateStatus Status { get; set; }

    public DateTime EstimateDateUtc { get; set; }

    public DateTime? ExpiryDateUtc { get; set; }

    public string? Notes { get; set; }

    // Totals
    public decimal Subtotal { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal TotalAmount { get; set; }

    // Audit
    public DateTime CreatedUtc { get; set; }

    public DateTime ModifiedUtc { get; set; } = DateTime.UtcNow;

    public List<EstimateItem> Items { get; set; } = new();
}