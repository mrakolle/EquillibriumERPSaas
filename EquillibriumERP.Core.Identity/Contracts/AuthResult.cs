namespace EquillibriumERP.Core.Identity.Application.Responses;

public class AuthResult
{
    public string AccessToken { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }

    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }

    public List<string> Roles { get; set; } = new();
    public List<string> Permissions { get; set; } = new();
}