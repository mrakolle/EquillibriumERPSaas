using System.Reflection;

namespace EquillibriumERP.Abstractions.Modules;

public interface IModuleAssemblyProvider
{
    IEnumerable<Assembly> GetAssemblies();
}