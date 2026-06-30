using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Manufacturing.Domain.Entities;

namespace EquillibriumERP.Manufacturing.Infrastructure.Configurations;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.ToTable("WorkOrders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PlannedQuantity)
            .HasPrecision(18, 4);

        builder.Property(x => x.UnitOfMeasure)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>();

        // BOM RELATIONSHIP
        builder.HasOne(x => x.BillOfMaterial)
            .WithMany()
            .HasForeignKey(x => x.BillOfMaterialId)
            .OnDelete(DeleteBehavior.Restrict);

        // MATERIALS
        builder.HasMany(x => x.Materials)
            .WithOne(x => x.WorkOrder)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // STEPS (CRITICAL - WAS MISSING)
        builder.HasMany(x => x.Steps)
            .WithOne(x => x.WorkOrder)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // CONSUMPTIONS
        builder.HasMany(x => x.MaterialConsumptions)
            .WithOne(x => x.WorkOrder)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // BATCHES
        builder.HasMany(x => x.ProductBatches)
            .WithOne(x => x.WorkOrder)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}