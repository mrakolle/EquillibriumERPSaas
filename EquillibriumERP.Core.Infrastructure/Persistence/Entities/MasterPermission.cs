namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class MasterPermission
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public ICollection<MasterRolePermission> RolePermissions { get; set; }
        = new List<MasterRolePermission>();
}