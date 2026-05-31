namespace EquillibriumERP.ControlPlane.Contracts.Responses;

public sealed record UserResponse(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    bool IsActive,
    List<Guid> RoleIds
);