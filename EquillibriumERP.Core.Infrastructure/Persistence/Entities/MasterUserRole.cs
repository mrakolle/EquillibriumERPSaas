namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class MasterUserRole
{
    public Guid UserId { get; set; }
    public MasterUser User { get; set; } = default!;

    public Guid RoleId { get; set; }
    public MasterRole Role { get; set; } = default!;
}