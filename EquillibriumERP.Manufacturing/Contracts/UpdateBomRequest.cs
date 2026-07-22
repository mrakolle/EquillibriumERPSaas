namespace EquillibriumERP.Manufacturing.Contracts;
public class UpdateBomRequest
{
    public Guid ProductId { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public List<CreateBomItemRequest> Items { get; set; } = new();

    public List<CreateBOMStepRequest> Steps { get; set; } = new();
}