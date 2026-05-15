namespace EquillibriumERP.Sales.Domain.Entities;

public class EstimateItem
{
    public Guid Id { get; private set; }

    public Guid EstimateId { get; private set; }

    public Guid ProductId { get; private set; }

    public string Description { get; private set; } = null!;

    public decimal Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal TaxRate { get; private set; }

    public decimal LineSubtotal { get; private set; }

    public decimal TaxAmount { get; private set; }

    public decimal LineTotal { get; private set; }

    private EstimateItem() { }

    public EstimateItem(
        Guid productId,
        string description,
        decimal quantity,
        decimal unitPrice,
        decimal taxRate)
    {
        Id = Guid.NewGuid();

        ProductId = productId;

        Description = description;

        Quantity = quantity;

        UnitPrice = unitPrice;

        TaxRate = taxRate;

        Calculate();
    }

    private void Calculate()
    {
        LineSubtotal = Quantity * UnitPrice;

        TaxAmount = LineSubtotal * (TaxRate / 100m);

        LineTotal = LineSubtotal + TaxAmount;
    }
}