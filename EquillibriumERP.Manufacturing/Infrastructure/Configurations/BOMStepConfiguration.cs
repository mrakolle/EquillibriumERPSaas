using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Manufacturing.Domain.Entities;

public class BOMStepConfiguration : IEntityTypeConfiguration<BOMStep>
{
    public void Configure(EntityTypeBuilder<BOMStep> builder)
    {
        builder.ToTable("BOMSteps");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BillOfMaterialId)
            .IsRequired();

        builder.Property(x => x.StepNumber)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(x => x.Duration)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.RawMaterialProductId)
            .IsRequired(false);

        builder.Property(x => x.QuantityPercentage)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.HasIndex(x => x.BillOfMaterialId);

        builder.HasIndex(x => new { x.BillOfMaterialId, x.StepNumber })
            .IsUnique();
    }
}