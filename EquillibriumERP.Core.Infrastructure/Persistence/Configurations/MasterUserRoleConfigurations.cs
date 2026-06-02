using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Core.Infrastructure.Persistence.Entities;

namespace EquillibriumERP.Core.Infrastructure.Persistence.Configurations;

public class MasterUserRoleConfiguration : IEntityTypeConfiguration<MasterUserRole>
{
    public void Configure(EntityTypeBuilder<MasterUserRole> builder)
    {
        builder.ToTable("MasterUserRoles");

        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.RoleId);
    }
}