using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Inventory.Domain.Entities;

namespace EquillibriumERP.Inventory;

public class InventoryDbContext : DbContext
{
    private readonly ITenantSession _tenantSession;

    public InventoryDbContext(
        DbContextOptions<InventoryDbContext> options,
        ITenantSession tenantSession)
        : base(options)
    {
        _tenantSession = tenantSession;
    }

    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();

    public DbSet<InventoryTransaction> InventoryTransactions => Set<InventoryTransaction>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(
            typeof(InventoryDbContext).Assembly);
    }
}