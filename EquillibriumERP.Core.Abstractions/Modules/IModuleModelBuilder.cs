using Microsoft.EntityFrameworkCore;

namespace EquillibriumERP.Core.Abstractions.Modules;
public interface IModuleModelBuilder
{
    void Configure(ModelBuilder builder);
}