namespace EquillibriumERP.Manufacturing.Application.Features.BillOfMaterials;

public class GetBillOfMaterialsResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
}