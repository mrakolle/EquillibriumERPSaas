using System.Reflection;

namespace EquillibriumERP.Core.Abstractions.Modules;

public static class ModuleRegistry
{
    public static List<IModule> GetModules()
    {
        var moduleInterface = typeof(IModule);

        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a =>
            {
                try { return a.GetTypes(); }
                catch { return Array.Empty<Type>(); }
            })
            .Where(t =>
                moduleInterface.IsAssignableFrom(t) &&
                !t.IsInterface &&
                !t.IsAbstract)
            .Select(t => Activator.CreateInstance(t))
            .OfType<IModule>()
            .ToList();
    }
}