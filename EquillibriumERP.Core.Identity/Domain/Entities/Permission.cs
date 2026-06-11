
namespace EquillibriumERP.Core.Identity.Domain.Entities;

public class Permission 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}