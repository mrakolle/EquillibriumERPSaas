using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Manufacturing.Infrastructure.Persistence.Configurations
{
    public class BillOfMaterialItemConfiguration : IEntityTypeConfiguration<BillOfMaterialItem>
    {
        public void Configure(EntityTypeBuilder<BillOfMaterialItem> builder)
        {
            builder.ToTable("BillOfMaterialItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.BillOfMaterialId)
                   .IsRequired();

            builder.Property(x => x.RawMaterialId)
                   .IsRequired();

            builder.Property(x => x.Quantity)
                   .IsRequired()
                   .HasPrecision(18, 4); // ERP-grade precision

            builder.Property(x => x.Unit)
                   .HasMaxLength(20);

            builder.Property(x => x.IsOptional)
                   .IsRequired();

            // Relationship: Item → BOM
            builder.HasOne(x => x.BillOfMaterial)
                   .WithMany(x => x.Items)
                   .HasForeignKey(x => x.BillOfMaterialId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes (critical for BOM explosion queries later)
            builder.HasIndex(x => x.BillOfMaterialId);
            builder.HasIndex(x => x.RawMaterialId);
        }
    }
}