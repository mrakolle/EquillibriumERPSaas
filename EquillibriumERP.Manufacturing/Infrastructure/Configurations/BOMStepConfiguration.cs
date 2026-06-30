using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Manufacturing.Domain.Entities;

namespace EquillibriumERP.Manufacturing.Infrastructure.Configurations;

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
            .HasColumnType("text");

        builder.Property(x => x.DurationMinutes)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        // NEW
        builder.Property(x => x.RawMaterialProductId);

        // NEW
        builder.Property(x => x.QuantityPercentage)
            .HasPrecision(18, 4);
    }
}