using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;

namespace EquillibriumERP.Core.Infrastructure.Persistence;

public class MasterDbContext : DbContext
{
    public MasterDbContext(DbContextOptions<MasterDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Feature> Features => Set<Feature>();
    public DbSet<TenantFeature> TenantFeatures => Set<TenantFeature>();
    public DbSet<BillingProfile> BillingProfiles => Set<BillingProfile>();
    public DbSet<BillingInvoice> BillingInvoices => Set<BillingInvoice>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<ApiKey> ApiKeys => Set<ApiKey>();
    public DbSet<Webhook> Webhooks => Set<Webhook>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MasterDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}