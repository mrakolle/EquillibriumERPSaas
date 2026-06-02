namespace EquillibriumERP.ControlPlane.Contracts.Responses;
public sealed record RoleResponse(
    Guid Id,
    string Name,
    IReadOnlyList<string> Permissions);