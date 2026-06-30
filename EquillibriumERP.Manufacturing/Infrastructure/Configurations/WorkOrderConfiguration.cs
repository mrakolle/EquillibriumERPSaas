using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Manufacturing.Domain.Entities;

public class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.ToTable("WorkOrders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BillOfMaterialId)
            .IsRequired();

        builder.Property(x => x.PlannedQuantity)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.UnitOfMeasure)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CompletedAt);

        builder.Property(x => x.BatchNo)
            .HasMaxLength(50);

        builder.Property(x => x.LotNo)
            .HasMaxLength(50);

        // Relationships

        builder.HasOne(x => x.BillOfMaterial)
            .WithMany()
            .HasForeignKey(x => x.BillOfMaterialId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Materials)
            .WithOne(x => x.WorkOrder)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Steps)
            .WithOne(x => x.WorkOrder)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.MaterialConsumptions)
            .WithOne(x => x.WorkOrder)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.ProductBatches)
            .WithOne(x => x.WorkOrder)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes

        builder.HasIndex(x => x.BillOfMaterialId);

        builder.HasIndex(x => x.Status);

        builder.HasIndex(x => x.BatchNo);

        builder.HasIndex(x => x.LotNo);
    }
}