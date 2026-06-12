namespace EquillibriumERP.Purchasing.Contracts;

public class CreatePurchaseOrderRequest
{
    public Guid SupplierId { get; set; }

    public DateTime OrderDateUtc { get; set; }

    public List<CreatePurchaseOrderLineRequest> Lines { get; set; } = new();
}