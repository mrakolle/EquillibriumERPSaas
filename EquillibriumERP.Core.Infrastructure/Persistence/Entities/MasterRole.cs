namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class MasterRole
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    public ICollection<MasterUserRole> UserRoles { get; set; } = new List<MasterUserRole>();

    public ICollection<MasterRolePermission> RolePermissions { get; set; } = new List<MasterRolePermission>();
}