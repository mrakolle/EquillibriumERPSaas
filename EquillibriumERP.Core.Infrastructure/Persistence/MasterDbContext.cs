using Microsoft.EntityFrameworkCore;
//using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;

namespace EquillibriumERP.Core.Infrastructure.Persistence;

public class MasterDbContext : DbContext
{
    public MasterDbContext(DbContextOptions<MasterDbContext> options)
        : base(options)
    {
    }

    //===================================================================================
    // Master data entities
    //===================================================================================
   /* public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<MasterUser> MasterUsers => Set<MasterUser>();
    public DbSet<Feature> Features => Set<Feature>();
    public DbSet<TenantFeature> TenantFeatures => Set<TenantFeature>();

    //===================================================================================
    // Billing and subscription related entities
    //===================================================================================
    public DbSet<BillingProfile> BillingProfiles => Set<BillingProfile>();
    public DbSet<BillingInvoice> BillingInvoices => Set<BillingInvoice>();
    public DbSet<Payment> Payments => Set<Payment>();

    //===================================================================================
    // API keys, webhooks, and audit logs
    //===================================================================================
    public DbSet<ApiKey> ApiKeys => Set<ApiKey>();
    public DbSet<Webhook> Webhooks => Set<Webhook>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<MasterRole> MasterRoles => Set<MasterRole>();
    public DbSet<MasterPermission> MasterPermissions => Set<MasterPermission>();
    public DbSet<MasterUserRole> MasterUserRoles => Set<MasterUserRole>();
    public DbSet<MasterRolePermission> MasterRolePermissions => Set<MasterRolePermission>(); */


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MasterDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    // MASTER DATA
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<MasterUser> MasterUsers { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<TenantFeature> TenantFeatures { get; set; }

    // BILLING
    public DbSet<BillingProfile> BillingProfiles { get; set; }
    public DbSet<BillingInvoice> BillingInvoices { get; set; }
    public DbSet<Payment> Payments { get; set; }

    // SECURITY / AUDIT
    public DbSet<ApiKey> ApiKeys { get; set; }
    public DbSet<Webhook> Webhooks { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<MasterRole> MasterRoles { get; set; }
    public DbSet<MasterPermission> MasterPermissions { get; set; }
    public DbSet<MasterUserRole> MasterUserRoles { get; set; }
    public DbSet<MasterRolePermission> MasterRolePermissions { get; set; }
}