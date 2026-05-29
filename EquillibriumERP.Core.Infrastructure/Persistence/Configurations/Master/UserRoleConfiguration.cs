using EquillibriumERP.Core.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Core.Infrastructure.Persistence.Configurations.Master;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.Property(x => x.AssignedAtUtc)
               .IsRequired();

        builder.HasOne(x => x.User)
               .WithMany(u => u.UserRoles)
               .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Role)
               .WithMany(r => r.UserRoles)
               .HasForeignKey(x => x.RoleId);
    }
}