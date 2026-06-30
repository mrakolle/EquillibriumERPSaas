namespace EquillibriumERP.Purchasing.Domain.Entities;
public class PurchaseOrderLine
{
    public Guid Id { get; set; }

    public Guid PurchaseOrderId { get; set; }

    public Guid ProductId { get; set; }

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal LineTotal { get; set; }

    public decimal QuantityReceived { get; set; }  

    public decimal QuantityOutstanding =>
        Quantity - QuantityReceived;

    public PurchaseOrder PurchaseOrder { get; set; } = null!;
}