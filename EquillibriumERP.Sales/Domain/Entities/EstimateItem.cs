namespace EquillibriumERP.Sales.Domain.Entities;

public class EstimateItem
{
    public Guid Id { get; set; }

    public Guid EstimateId { get; set; }

    public Estimate Estimate { get; set; } = null!;

    public Guid ProductId { get; set; }

    // Snapshots
    public string ProductCode { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string UnitOfMeasure { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal DiscountPercent { get; set; }

    public decimal TaxRate { get; set; }

    public decimal LineSubtotal { get; set; }

    public decimal TaxAmount { get; set; }

    public decimal LineTotal { get; set; }
}