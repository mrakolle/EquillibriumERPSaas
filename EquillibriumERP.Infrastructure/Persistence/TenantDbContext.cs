using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Diagnostics;
using EquillibriumERP.Abstractions.Modules;
using EquillibriumERP.Abstractions.MultiTenancy;

namespace EquillibriumERP.Infrastructure.Persistence;

public class TenantDbContext : DbContext
{
    private readonly IModuleAssemblyProvider _moduleAssemblyProvider;
    private readonly ITenantResolver _tenantResolver;

    public string Schema => _tenantResolver.GetSchema();

    public TenantDbContext(
        DbContextOptions<TenantDbContext> options,
        IModuleAssemblyProvider moduleAssemblyProvider,
        ITenantResolver tenantResolver)
        : base(options)
    {
        _moduleAssemblyProvider = moduleAssemblyProvider;
        _tenantResolver = tenantResolver;

        EfContextGuard.Validate(this);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder
            .ReplaceService<IModelCacheKeyFactory, TenantModelCacheKeyFactory>()
            .ConfigureWarnings(w =>
                w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var assemblies = _moduleAssemblyProvider.GetAssemblies();

        if (assemblies == null)
            return;

        foreach (var assembly in assemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}

   /* protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ❗ DO NOT hardcode schema here anymore
        modelBuilder.HasDefaultSchema(Schema);

        var assemblies = _moduleAssemblyProvider.GetAssemblies();

        if (assemblies == null)
            return;

        foreach (var assembly in assemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}*/
internal class TenantModelCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime)
    {
        var tenantContext = (TenantDbContext)context;

        return (
            context.GetType(),
            tenantContext.Schema,
            designTime
        );
    }
}