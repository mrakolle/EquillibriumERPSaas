using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Manufacturing.Infrastructure.Configurations;

public sealed class BillOfMaterialConfiguration
    : IEntityTypeConfiguration<BillOfMaterial>
{
    public void Configure(EntityTypeBuilder<BillOfMaterial> builder)
    {
        builder.ToTable("BillOfMaterials");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasMany(x => x.Items)
              .WithOne(x => x.BillOfMaterial)
              .HasForeignKey(x => x.BillOfMaterialId)
              .OnDelete(DeleteBehavior.Cascade);
    }
}