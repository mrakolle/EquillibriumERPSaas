using System.Reflection;

namespace EquillibriumERP.Core.Abstractions.Modules;

public interface IModuleAssemblyProvider
{
    IEnumerable<Assembly> GetAssemblies();
}