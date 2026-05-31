namespace EquillibriumERP.ControlPlane.Contracts.Requests;

public sealed record UpdateUserRequest(
    string Email,
    string FirstName,
    string LastName,
    bool IsActive,
    List<Guid> RoleIds
);