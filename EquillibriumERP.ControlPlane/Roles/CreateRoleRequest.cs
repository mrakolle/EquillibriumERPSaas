namespace EquillibriumERP.ControlPlane.Application.Roles;

public sealed record CreateRoleRequest(
    string Name,
    List<string> Permissions);