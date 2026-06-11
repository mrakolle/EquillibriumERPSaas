using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Sales.Domain.Entities;

namespace EquillibriumERP.Sales;

public class SalesDbContext : DbContext
{
    private readonly ITenantSession _tenantSession;

    public SalesDbContext(
        DbContextOptions<SalesDbContext> options,
        ITenantSession tenantSession)
        : base(options)
    {
        _tenantSession = tenantSession;
    }

    // Estimates
    public DbSet<Estimate> Estimates => Set<Estimate>();
    public DbSet<EstimateItem> EstimateItems => Set<EstimateItem>();
    public DbSet<EstimateSequence> EstimateSequences => Set<EstimateSequence>();
    public DbSet<EstimateSettings> EstimateSettings => Set<EstimateSettings>();

    // Invoices
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<InvoiceSequence> InvoiceSequences => Set<InvoiceSequence>();
    public DbSet<InvoicePayment> InvoicePayments => Set<InvoicePayment>();
    public DbSet<InvoiceSettings> InvoiceSettings => Set<InvoiceSettings>();

    // Customers
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();
    public DbSet<CustomerContact> CustomerContacts => Set<CustomerContact>();
    public DbSet<CustomerCategory> CustomerCategories => Set<CustomerCategory>();
    public DbSet<CustomerCreditProfile> CustomerCreditProfiles => Set<CustomerCreditProfile>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
    }
}