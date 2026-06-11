using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.Modules;
public sealed class ProductsModelBuilder : IModuleModelBuilder
{
    public void Configure(ModelBuilder builder)
    {
       /* builder.ApplyConfigurationsFromAssembly(
            typeof(ProductsDbContext).Assembly);*/
    }
}