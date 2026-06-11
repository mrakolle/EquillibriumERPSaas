using System.Reflection;
using EquillibriumERP.Core.Abstractions.Modules;

namespace EquillibriumERP.Core.Infrastructure.Modules;

public class ModuleAssemblyProvider : IModuleAssemblyProvider
{
    public IEnumerable<Assembly> GetAssemblies()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a =>
                a.FullName != null &&
                a.FullName.Contains(".Infrastructure") &&
                !a.FullName.Contains("EquillibriumERP.Core.Infrastructure"));

        /// Old implemntation 
        /*return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a =>
                a.FullName != null &&
                a.FullName.StartsWith("EquillibriumERP."));*/
    }
}


/*using System.Reflection;
using EquillibriumERP.Core.Abstractions.Modules;

namespace EquillibriumERP.Core.Infrastructure.Modules;

public class ModuleAssemblyProvider : IModuleAssemblyProvider
{
    public IEnumerable<Assembly> GetAssemblies()
    {
        return new[]
        {
            typeof(EquillibriumERP.Sales.ModuleMarker).Assembly,
            typeof(EquillibriumERP.Inventory.ModuleMarker).Assembly,
            typeof(EquillibriumERP.Manufacturing.ModuleMarker).Assembly,
            typeof(EquillibriumERP.Quality.ModuleMarker).Assembly,
            //typeof(EquillibriumERP.MES.ModuleMarker).Assembly,
            //typeof(EquillibriumERP.IoT.ModuleMarker).Assembly
        };
    }
}*/