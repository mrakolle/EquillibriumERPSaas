using System.Reflection;
using EquillibriumERP.Abstractions.Modules;

namespace EquillibriumERP.Infrastructure.Modules;

public class ModuleAssemblyProvider : IModuleAssemblyProvider
{
    public IEnumerable<Assembly> GetAssemblies()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .Where(a =>
                a.FullName != null &&
                a.FullName.StartsWith("EquillibriumERP."));
    }
}


/*using System.Reflection;
using EquillibriumERP.Abstractions.Modules;

namespace EquillibriumERP.Infrastructure.Modules;

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