using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EquillibriumERP.Manufacturing.Domain.Entities;

public class WorkOrderMaterialConfiguration : IEntityTypeConfiguration<WorkOrderMaterial>
{
    public void Configure(EntityTypeBuilder<WorkOrderMaterial> builder)
    {
        builder.ToTable("WorkOrderMaterials");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.WorkOrderId)
            .IsRequired();

        builder.Property(x => x.RawMaterialProductId)
            .IsRequired();

        builder.Property(x => x.ExpectedQuantity)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.IssuedQuantity)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.ConsumedQuantity)
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.UnitOfMeasure)
            .IsRequired()
            .HasMaxLength(20);

        // Relationship

        builder.HasOne(x => x.WorkOrder)
            .WithMany(x => x.Materials)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes

        builder.HasIndex(x => x.WorkOrderId);

        builder.HasIndex(x => x.RawMaterialProductId);
    }
}