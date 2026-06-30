namespace EquillibriumERP.Core.Identity.Contracts.Auth;

public sealed class LoginRequest
{
    public string TenantCode { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}