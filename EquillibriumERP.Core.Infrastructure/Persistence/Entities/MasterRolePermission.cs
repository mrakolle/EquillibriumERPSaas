namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class MasterRolePermission
{
    public Guid RoleId { get; set; }
    public MasterRole Role { get; set; } = default!;

    public Guid PermissionId { get; set; }
    public MasterPermission Permission { get; set; } = default!;
}