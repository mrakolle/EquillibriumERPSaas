namespace EquillibriumERP.Manufacturing.Application.Features.BillOfMaterials;

public class CreateBillOfMaterialRequest
{
    public Guid ProductId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<CreateBillOfMaterialItemRequest> Items { get; set; } = new();
}