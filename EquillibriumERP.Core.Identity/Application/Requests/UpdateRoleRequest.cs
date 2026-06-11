namespace EquillibriumERP.Identity.Application.Requests;

public sealed record UpdateRoleRequest(
    string Name,
    string Description,
    IReadOnlyList<string> Permissions
);