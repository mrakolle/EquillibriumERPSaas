namespace EquillibriumERP.Manufacturing.Contracts;

public class CreateBomRequest
{
    public Guid ProductId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<CreateBomItemRequest> Items { get; set; } = new();
}