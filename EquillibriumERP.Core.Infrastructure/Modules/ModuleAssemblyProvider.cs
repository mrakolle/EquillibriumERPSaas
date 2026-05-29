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
            typeof(EquillibriumERP.Sales.Infrastructure.ModuleMarker).Assembly,
            typeof(EquillibriumERP.Inventory.Infrastructure.ModuleMarker).Assembly,
            typeof(EquillibriumERP.Manufacturing.Infrastructure.ModuleMarker).Assembly,
            typeof(EquillibriumERP.Quality.Infrastructure.ModuleMarker).Assembly,
            //typeof(EquillibriumERP.MES.Infrastructure.ModuleMarker).Assembly,
            //typeof(EquillibriumERP.IoT.Infrastructure.ModuleMarker).Assembly
        };
    }
}*/