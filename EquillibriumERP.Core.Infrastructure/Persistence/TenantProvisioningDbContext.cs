using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.Modules;

namespace EquillibriumERP.Core.Infrastructure.Persistence;
public class TenantProvisioningDbContext : DbContext
{
    private readonly IEnumerable<IModuleModelBuilder> _modelBuilders;
    private readonly string _schema;

    public TenantProvisioningDbContext(
        DbContextOptions<TenantProvisioningDbContext> options,
        IEnumerable<IModuleModelBuilder> modelBuilders,
        string schema)
        : base(options)
    {
        Console.WriteLine($"TenantProvisioningDbContext created with schema: {schema}");
        _modelBuilders = modelBuilders;
        _schema = schema;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(_schema);

        foreach (var module in _modelBuilders)
        {
            Console.WriteLine($"Configuring model for module: {module.GetType().Name}, schema: {_schema}");
            module.Configure(builder);
        }

        base.OnModelCreating(builder);
    }
}