using System.Linq;
using EquillibriumERP.Core.Abstractions;
using EquillibriumERP.Core.Abstractions.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace EquillibriumERP.Core.Infrastructure.Authorization;

public static class PermissionRegistry
{
    public static IReadOnlyList<PermissionDefinition> GetAll(
        IServiceProvider sp)
    {
        return sp.GetServices<IModulePermissionProvider>()
                 .SelectMany(x => x.GetPermissions())
                 .DistinctBy(x => x.Code)
                 .ToList();
    }
}