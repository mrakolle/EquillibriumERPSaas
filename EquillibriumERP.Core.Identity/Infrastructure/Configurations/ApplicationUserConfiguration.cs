using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Core.Identity.Infrastructure.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // -----------------------------
        // Table
        // -----------------------------
        builder.ToTable("Users");

        // -----------------------------
        // Keys (Identity already configures Id)
        // -----------------------------
        builder.HasKey(x => x.Id);

        // -----------------------------
        // Properties
        // -----------------------------
        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.Property(x => x.TenantId)
            .IsRequired();

        // -----------------------------
        // Indexes (multi-tenant critical)
        // -----------------------------
        builder.HasIndex(x => x.TenantId);

        builder.HasIndex(x => new { x.TenantId, x.Email })
            .IsUnique();

        builder.HasIndex(x => new { x.TenantId, x.UserName })
            .IsUnique();

        // -----------------------------
        // Relationships (Identity handles most internally)
        // -----------------------------
        // We intentionally avoid overriding IdentityUserRole mappings here
        // to prevent breaking Identity internals.
    }
}