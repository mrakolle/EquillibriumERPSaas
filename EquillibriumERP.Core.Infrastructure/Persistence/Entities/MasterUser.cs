namespace EquillibriumERP.Core.Infrastructure.Persistence.Entities;

public class MasterUser
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string TenantCode { get; set; } = default!;
    public bool IsSysAdmin { get; set; }

    public ICollection<MasterUserRole> UserRoles { get; set; } = new List<MasterUserRole>();
}