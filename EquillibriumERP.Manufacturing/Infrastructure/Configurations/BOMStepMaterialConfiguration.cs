using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Manufacturing.Domain.Entities;

public class BOMStepMaterialConfiguration : IEntityTypeConfiguration<BOMStepMaterial>
{
    public void Configure(EntityTypeBuilder<BOMStepMaterial> builder)
    {
        builder.ToTable("BOMStepMaterials");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BOMStepId)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasPrecision(18, 4);

        builder.Property(x => x.RawMaterialProductId)
            .IsRequired();

        builder.HasOne<BOMStep>()
            .WithMany()
            .HasForeignKey(x => x.BOMStepId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.BOMStepId);
        builder.HasIndex(x => x.RawMaterialProductId);
    }
}