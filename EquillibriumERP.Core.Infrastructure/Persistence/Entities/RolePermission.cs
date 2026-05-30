namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class RolePermission
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }

    public string Code { get; set; } = string.Empty;

    public Role Role { get; set; } = null!;
}