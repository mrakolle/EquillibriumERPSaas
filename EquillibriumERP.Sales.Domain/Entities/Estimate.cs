using EquillibriumERP.Sales.Domain.Enums;

namespace EquillibriumERP.Sales.Domain.Entities;

public class Estimate
{
    public Guid Id { get; private set; }

    public string EstimateNumber { get; private set; } = null!;

    public Guid CustomerId { get; private set; }

    public DateTime EstimateDateUtc { get; private set; }

    public DateTime ExpiryDateUtc { get; private set; }

    public EstimateStatus Status { get; private set; }

    public decimal Subtotal { get; private set; }

    public decimal TaxAmount { get; private set; }

    public decimal TotalAmount { get; private set; }

    public string Notes { get; private set; } = string.Empty;

    // Navigation
    public ICollection<EstimateItem> Items { get; private set; }
        = new List<EstimateItem>();

    private Estimate() { }

    public Estimate(
        string estimateNumber,
        Guid customerId,
        DateTime estimateDateUtc,
        DateTime expiryDateUtc,
        string? notes = null)
    {
        Id = Guid.NewGuid();

        EstimateNumber = estimateNumber;

        CustomerId = customerId;

        EstimateDateUtc = estimateDateUtc;

        ExpiryDateUtc = expiryDateUtc;

        Status = EstimateStatus.Draft;

        Notes = notes ?? string.Empty;
    }

    public void AddItem(EstimateItem item)
    {
        Items.Add(item);

        RecalculateTotals();
    }

    public void MarkAsSent()
    {
        Status = EstimateStatus.Sent;
    }

    public void Approve()
    {
        Status = EstimateStatus.Approved;
    }

    public void Reject()
    {
        Status = EstimateStatus.Rejected;
    }

    public void Convert()
    {
        Status = EstimateStatus.Converted;
    }

    private void RecalculateTotals()
    {
        Subtotal = Items.Sum(x => x.LineSubtotal);

        TaxAmount = Items.Sum(x => x.TaxAmount);

        TotalAmount = Subtotal + TaxAmount;
    }
}