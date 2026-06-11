namespace EquillibriumERP.Manufacturing.Application.Features.BillOfMaterials;

public class CreateBillOfMaterialItemRequest
{
    public Guid RawMaterialId { get; set; }
    public decimal Quantity { get; set; }
    public string? Unit { get; set; }
    public bool IsOptional { get; set; }
}