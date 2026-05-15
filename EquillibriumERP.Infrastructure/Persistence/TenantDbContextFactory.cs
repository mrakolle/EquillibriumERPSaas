using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EquillibriumERP.Infrastructure.Modules;
using EquillibriumERP.Abstractions.MultiTenancy;
using EquillibriumERP.Abstractions.Modules;

namespace EquillibriumERP.Infrastructure.Persistence;

public class TenantDbContextFactory
{
    private readonly IConfiguration _configuration;
    private readonly ITenantResolver _tenantResolver;
    private readonly IModuleAssemblyProvider _moduleProvider;

    public TenantDbContextFactory(
        IConfiguration configuration,
        ITenantResolver tenantResolver,
        IModuleAssemblyProvider moduleProvider)
    {
        _configuration = configuration;
        _tenantResolver = tenantResolver;
        _moduleProvider = moduleProvider;
    }

    public TenantDbContext Create()
    {
        var schema = _tenantResolver.GetSchema();

        if (string.IsNullOrWhiteSpace(schema))
            throw new InvalidOperationException("Tenant schema not resolved");

        var options = BuildOptions(schema);

        var context = new TenantDbContext(options, _moduleProvider, _tenantResolver);

        ApplySchema(context, schema);

        return context;
    }

    private DbContextOptions<TenantDbContext> BuildOptions(string schema)
    {
        var connectionString = _configuration.GetConnectionString("TenantDatabase");

        return new DbContextOptionsBuilder<TenantDbContext>()
            .UseNpgsql(connectionString, x =>
            {
                x.MigrationsHistoryTable("__EFMigrationsHistory", schema);
            })
            .Options;
    }

    private static void ApplySchema(TenantDbContext context, string schema)
    {
        var conn = context.Database.GetDbConnection();

        if (conn.State != System.Data.ConnectionState.Open)
            conn.Open();

        using var command = conn.CreateCommand();

        command.CommandText = $"SET search_path TO \"{schema}\", public;";
        command.ExecuteNonQuery();
    }
}