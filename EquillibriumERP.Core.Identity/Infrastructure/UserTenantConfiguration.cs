using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Identity.Infrastructure.Configurations;

public class UserTenantConfiguration : IEntityTypeConfiguration<UserTenant>
{
    public void Configure(EntityTypeBuilder<UserTenant> builder)
    {
        // -----------------------------
        // Table
        // -----------------------------
        builder.ToTable("UserTenants");

        // -----------------------------
        // Composite Key
        // (prevents duplicate user-tenant assignments)
        // -----------------------------
        builder.HasKey(x => new { x.UserId, x.TenantId });

        // -----------------------------
        // Relationships: User
        // -----------------------------
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // -----------------------------
        // Indexes (critical for tenant resolution)
        // -----------------------------
        builder.HasIndex(x => x.TenantId);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => new { x.UserId, x.TenantId })
            .IsUnique();

        // -----------------------------
        // Notes:
        // This table defines WHICH tenants a user belongs to.
        // It does NOT replace ApplicationUser.TenantId.
        // It complements it for multi-tenant ERP flexibility.
        // -----------------------------
    }
}