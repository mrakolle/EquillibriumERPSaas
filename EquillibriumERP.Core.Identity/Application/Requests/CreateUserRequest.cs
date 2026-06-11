namespace EquillibriumERP.Identity.Application.Requests;

public sealed record CreateUserRequest(
    string Email,
    string FirstName,
    string LastName,
    List<Guid> RoleIds
);