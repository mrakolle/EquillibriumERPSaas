using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Manufacturing.Infrastructure.Configurations;

public class WorkOrderStepConfiguration : IEntityTypeConfiguration<WorkOrderStep>
{
    public void Configure(EntityTypeBuilder<WorkOrderStep> builder)
    {
        builder.ToTable("WorkOrderSteps");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Action)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>();

        builder.Property(x => x.StepNumber)
            .IsRequired();

        builder.HasOne(x => x.WorkOrder)
            .WithMany(x => x.Steps)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.WorkOrderMaterial)
            .WithMany()
            .HasForeignKey(x => x.WorkOrderMaterialId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}