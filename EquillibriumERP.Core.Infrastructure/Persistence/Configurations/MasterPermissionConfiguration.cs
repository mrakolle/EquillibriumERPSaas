using EquillibriumERP.Core.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Core.Infrastructure.Persistence.Configurations;

public class MasterPermissionConfiguration
    : IEntityTypeConfiguration<MasterPermission>
{
    public void Configure(EntityTypeBuilder<MasterPermission> builder)
    {
        builder.ToTable("MasterPermissions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(x => x.Code)
            .IsUnique();
    }
}