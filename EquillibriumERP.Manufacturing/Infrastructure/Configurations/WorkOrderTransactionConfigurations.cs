using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Manufacturing.Domain.Entities;

public class WorkOrderTransactionConfiguration : IEntityTypeConfiguration<WorkOrderTransaction>
{
    public void Configure(EntityTypeBuilder<WorkOrderTransaction> builder)
    {
        builder.ToTable("WorkOrderTransactions");

        builder.HasKey(x => x.Id);

        // Core traceability
        builder.Property(x => x.WorkOrderId)
            .IsRequired();

        builder.Property(x => x.WorkOrderStepId)
            .IsRequired();

        builder.Property(x => x.WorkOrderMaterialId)
            .IsRequired(false);

        // Audit
        builder.Property(x => x.ExecutedAt)
            .IsRequired();

        builder.Property(x => x.ExecutedByUserId)
            .IsRequired();

        builder.Property(x => x.Workstation)
            .HasMaxLength(100);

        builder.Property(x => x.RawMaterialLotNo)
            .HasMaxLength(100);

        // Production values
        builder.Property(x => x.ExpectedQuantity)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.ActualQuantity)
            .HasPrecision(18, 4)
            .IsRequired();

        // Variance is computed in the entity, so ignore it
        builder.Ignore(x => x.Variance);

        builder.Property(x => x.UnitOfMeasure)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Comment)
            .HasColumnType("text");

        // Indexes
        builder.HasIndex(x => x.WorkOrderId);

        builder.HasIndex(x => x.WorkOrderStepId);

        builder.HasIndex(x => x.WorkOrderMaterialId);

        builder.HasIndex(x => x.ExecutedAt);
    }
}