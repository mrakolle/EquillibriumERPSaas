using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
//using EquillibriumERP.Core.Onboarding.Domain;

namespace EquillibriumERP.ControlPlane.Infrastructure.Persistence;

public class ControlPlaneDbContext : DbContext
{
    public ControlPlaneDbContext(
        DbContextOptions<ControlPlaneDbContext> options)
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
    public async Task<List<Tenant>> GetActiveTenantsAsync(CancellationToken ct)
    {
        return await Tenants
            .Where(t => t.IsActive)
            .Select(t => new Tenant
            {
                Id = t.Id,
                Code = t.Code,
                Schema = t.Schema
            })
            .ToListAsync(ct);
    }
    

}