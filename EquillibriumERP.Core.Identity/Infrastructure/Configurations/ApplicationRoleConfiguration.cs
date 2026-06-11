using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Core.Identity.Infrastructure.Configurations;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        // -----------------------------
        // Table
        // -----------------------------
        builder.ToTable("Roles");

        // -----------------------------
        // Key
        // -----------------------------
        builder.HasKey(x => x.Id);

        // -----------------------------
        // Properties
        // -----------------------------
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.NormalizedName)
            .HasMaxLength(256);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        // -----------------------------
        // Indexes (Identity standard)
        // -----------------------------
        builder.HasIndex(x => x.NormalizedName)
            .IsUnique()
            .HasDatabaseName("IX_Roles_NormalizedName");

        // -----------------------------
        // Optional: Tenant-aware role strategy (future-proofing)
        // -----------------------------
        // If later needed:
        // builder.Property<Guid?>("TenantId");
        // builder.HasIndex("TenantId");
    }
}