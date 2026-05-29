using System;

namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class UserRole
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public Guid RoleId { get; set; }
    public Role Role { get; set; } = default!;

    public DateTime AssignedAtUtc { get; set; }

    public Guid? AssignedByUserId { get; set; }
}