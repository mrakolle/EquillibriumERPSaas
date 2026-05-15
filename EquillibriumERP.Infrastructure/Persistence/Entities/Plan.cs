namespace EquillibriumERP.Infrastructure.Persistence.Entities;

public class Plan
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}