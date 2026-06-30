namespace EquillibriumERP.Core.Abstractions.Authorization;

public interface IPermissionService
{
    bool HasPermission(string userId, string permission);
}