namespace EquillibriumERP.Core.Abstractions.Identity;

public sealed class CreateUserResponse
{
    public Guid UserId { get; init; }

    public string Email { get; init; } = string.Empty;

    public string UserName { get; init; } = string.Empty;
}