namespace EquillibriumERP.Core.Identity.Contracts.Auth;

public sealed class LoginResponse
{
    public string AccessToken { get; set; } = default!;
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
}