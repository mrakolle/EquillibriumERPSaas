using EquillibriumERP.Purchasing.Domain.Enums;

namespace EquillibriumERP.Purchasing.Contracts;

public class PurchaseOrderResponse
{
    public Guid Id { get; set; }

    public string Number { get; set; } = string.Empty;

    public Guid SupplierId { get; set; }

    public DateTime OrderDateUtc { get; set; }

    public PurchaseOrderStatus Status { get; set; }
}