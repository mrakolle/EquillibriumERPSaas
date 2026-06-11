namespace EquillibriumERP.Core.Identity.Domain.Entities;

public class UserTenant
{
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;

    public Guid TenantId { get; set; }
}