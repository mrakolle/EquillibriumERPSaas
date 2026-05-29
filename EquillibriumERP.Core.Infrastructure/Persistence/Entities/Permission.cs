namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class Permission
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Code { get; set; } = default!;

    public string Description { get; set; } = default!;

    public Guid? RoleId { get; set; }

    public Role? Role { get; set; }
}