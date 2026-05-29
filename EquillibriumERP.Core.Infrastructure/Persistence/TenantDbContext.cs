using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Diagnostics;
using EquillibriumERP.Core.Abstractions.Modules;
using EquillibriumERP.Core.Abstractions.Persistence;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using System.Data;

namespace EquillibriumERP.Core.Infrastructure.Persistence;

public class TenantDbContext : DbContext, ITenantDbContext
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
        Console.WriteLine("Building model...");
         Console.WriteLine($"SCHEMA = {Schema}");
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schema);

        var assemblies = _moduleAssemblyProvider.GetAssemblies();

        if (assemblies == null)
            return;

        foreach (var assembly in assemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
       
    }
    public void EnsureTenantSchema()
    {
        var conn = Database.GetDbConnection();

        if (conn.State != ConnectionState.Open)
            conn.Open();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = $"SET search_path TO \"{Schema}\"";
        cmd.ExecuteNonQuery();
    }
}

 