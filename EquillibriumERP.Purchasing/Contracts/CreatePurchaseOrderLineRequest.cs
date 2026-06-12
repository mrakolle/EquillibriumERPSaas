namespace EquillibriumERP.Purchasing.Contracts;

public class CreatePurchaseOrderLineRequest
{
    public Guid ProductId { get; set; }

    public decimal Quantity { get; set; }
}