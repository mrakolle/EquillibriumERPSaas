using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Core.Identity.Infrastructure.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        // -----------------------------
        // Table
        // -----------------------------
        builder.ToTable("Permissions");

        // -----------------------------
        // Key
        // -----------------------------
        builder.HasKey(x => x.Id);

        // -----------------------------
        // Properties
        // -----------------------------
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        // -----------------------------
        // Constraints
        // -----------------------------
        builder.HasIndex(x => x.Name)
            .IsUnique();

        // -----------------------------
        // Optional ERP extension (future-safe)
        // -----------------------------
        // builder.Property<Guid?>("TenantId");
        // builder.HasIndex("TenantId");
    }
}