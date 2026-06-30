using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BillOfMaterialItemConfiguration : IEntityTypeConfiguration<BillOfMaterialItem>
{
    public void Configure(EntityTypeBuilder<BillOfMaterialItem> builder)
    {
        builder.ToTable("BillOfMaterialItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BillOfMaterialId)
            .IsRequired();

        builder.Property(x => x.RawMaterialProductId)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.UnitOfMeasure)
            .IsRequired()
            .HasMaxLength(20);

        // Relationship: Item → BOM
        builder.HasOne(x => x.BillOfMaterial)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.BillOfMaterialId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes (important for BOM performance)
        builder.HasIndex(x => x.BillOfMaterialId);

        builder.HasIndex(x => x.RawMaterialProductId);
    }
}