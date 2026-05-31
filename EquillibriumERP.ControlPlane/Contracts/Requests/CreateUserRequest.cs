namespace EquillibriumERP.ControlPlane.Contracts.Requests;

public sealed record CreateUserRequest(
    string Email,
    string FirstName,
    string LastName,
    List<Guid> RoleIds
);