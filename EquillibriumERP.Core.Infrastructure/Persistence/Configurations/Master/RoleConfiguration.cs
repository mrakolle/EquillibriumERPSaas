using EquillibriumERP.Core.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Core.Infrastructure.Persistence.Configurations.Master;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired();

        builder.Property(x => x.Description)
               .IsRequired();

        builder.HasMany(r => r.Permissions)
               .WithOne(p => p.Role)
               .HasForeignKey(p => p.RoleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}