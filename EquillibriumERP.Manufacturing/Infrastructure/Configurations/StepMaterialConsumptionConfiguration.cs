using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


public class StepMaterialConsumptionConfiguration : IEntityTypeConfiguration<StepMaterialConsumption>
{
    public void Configure(EntityTypeBuilder<StepMaterialConsumption> builder)
    {
        builder.ToTable("StepMaterialConsumptions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.QuantityUsed)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.RawMaterialProductId)
            .IsRequired();

        builder.Property(x => x.WorkOrderStepId)
            .IsRequired();

        builder.Property(x => x.RecordedAt)
            .IsRequired();

        builder.HasIndex(x => x.WorkOrderStepId);
        builder.HasIndex(x => x.RawMaterialProductId);
    }
}