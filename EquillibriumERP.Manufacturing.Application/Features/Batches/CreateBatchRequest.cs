namespace EquillibriumERP.Manufacturing.Application.Features.Batches;

public class CreateBatchRequest
{
    public Guid BillOfMaterialId { get; set; }
    public decimal Quantity { get; set; }
}