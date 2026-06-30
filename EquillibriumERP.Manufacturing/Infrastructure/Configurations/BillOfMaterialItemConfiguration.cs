using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Manufacturing.Infrastructure.Configurations;

public sealed class BillOfMaterialItemConfiguration
    : IEntityTypeConfiguration<BillOfMaterialItem>
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
            .HasMaxLength(20)
            .IsRequired();
    }
}