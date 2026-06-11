using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Identity.Infrastructure;

namespace EquillibriumERP.Core.Identity.Infrastructure;
public sealed class IdentityModelBuilder : IModuleModelBuilder
{
    public void Configure(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(
            typeof(IdentityDbContext).Assembly);
    }
}