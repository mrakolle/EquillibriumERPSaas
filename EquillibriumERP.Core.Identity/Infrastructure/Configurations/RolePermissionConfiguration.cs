using EquillibriumERP.Core.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Core.Identity.Infrastructure.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        // -----------------------------
        // Table
        // -----------------------------
        builder.ToTable("RolePermissions");

        // -----------------------------
        // Composite Key (many-to-many join)
        // -----------------------------
        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        // -----------------------------
        // Relationships: Role
        // -----------------------------
        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        // -----------------------------
        // Relationships: Permission
        // -----------------------------
        builder.HasOne(x => x.Permission)
            .WithMany()
            .HasForeignKey(x => x.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        // -----------------------------
        // Indexing (performance for auth checks)
        // -----------------------------
        builder.HasIndex(x => x.RoleId);

        builder.HasIndex(x => x.PermissionId);
    }
}