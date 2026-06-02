namespace EquillibriumERP.Core.Identity.Infrastructure.Entities;

public class UserRole
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public Guid RoleId { get; set; }
    public Role Role { get; set; } = default!;
}