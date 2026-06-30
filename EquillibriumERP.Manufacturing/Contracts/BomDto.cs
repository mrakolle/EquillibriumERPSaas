namespace EquillibriumERP.Manufacturing.Contracts;
public class BomDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ProductId { get; set; }

    public List<BomItemDto> Items { get; set; } = new();
}