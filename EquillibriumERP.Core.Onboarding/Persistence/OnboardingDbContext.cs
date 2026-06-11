using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Onboarding.Domain;

namespace EquillibriumERP.Core.Onboarding.Persistence;

public class OnboardingDbContext : DbContext
{
    public OnboardingDbContext(
        DbContextOptions<OnboardingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tenant> Tenants => Set<Tenant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tenant>();
    }
}