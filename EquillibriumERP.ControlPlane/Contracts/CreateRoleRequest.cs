namespace EquillibriumERP.ControlPlane.Application.Contracts;

public sealed record CreateRoleRequest(
    string Name,
    List<string> Permissions);