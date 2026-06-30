using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Manufacturing.Domain.Entities;

public class WorkOrderStepConfiguration : IEntityTypeConfiguration<WorkOrderStep>
{
    public void Configure(EntityTypeBuilder<WorkOrderStep> builder)
    {
        builder.ToTable("WorkOrderSteps");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.WorkOrderId)
            .IsRequired();

        builder.Property(x => x.BOMProcessStepId)
            .IsRequired();

        builder.Property(x => x.WorkOrderMaterialId)
            .IsRequired(false);

        builder.Property(x => x.StepNumber)
            .IsRequired();

        builder.Property(x => x.Action)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.StartedAt);

        builder.Property(x => x.CompletedAt);

        // Relationships

        builder.HasOne(x => x.WorkOrder)
            .WithMany(x => x.Steps)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.WorkOrderMaterial)
            .WithMany()
            .HasForeignKey(x => x.WorkOrderMaterialId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes

        builder.HasIndex(x => x.WorkOrderId);

        builder.HasIndex(x => x.BOMProcessStepId);

        builder.HasIndex(x => x.WorkOrderMaterialId);

        builder.HasIndex(x => new { x.WorkOrderId, x.StepNumber })
            .IsUnique();
    }
}