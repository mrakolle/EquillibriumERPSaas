namespace EquillibriumERP.Core.Identity.Application;

public class UserClaimsResult
{
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }

    public List<string> Roles { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
}