namespace EquillibriumERP.Core.Abstractions.Identity;

public sealed class LoginRequest
{
    public string TenantCode { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}