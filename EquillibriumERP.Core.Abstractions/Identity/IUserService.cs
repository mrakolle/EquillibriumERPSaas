namespace EquillibriumERP.Core.Abstractions.Identity;

public interface IUserService
{
    Task<CreateUserResponse> CreateAsync(
        CreateUserRequest request,
        CancellationToken ct = default);


}