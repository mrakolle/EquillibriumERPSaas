using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Products.Domain.Entities;


namespace EquillibriumERP.Products.Infrastructure;

public class ProductsDbContext : DbContext
{
    private readonly ITenantSession _tenantSession;

    public ProductsDbContext(
        DbContextOptions<ProductsDbContext> options,
        ITenantSession tenantSession)
        : base(options)
    {
        _tenantSession = tenantSession;
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ProductsDbContext).Assembly);
    }
}