using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Manufacturing.Infrastructure.Configurations;

public class WorkOrderMaterialConfiguration : IEntityTypeConfiguration<WorkOrderMaterial>
{
    public void Configure(EntityTypeBuilder<WorkOrderMaterial> builder)
    {
        builder.ToTable("WorkOrderMaterials");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExpectedQuantity)
            .HasPrecision(18, 4);

        builder.Property(x => x.IssuedQuantity)
            .HasPrecision(18, 4);

        builder.Property(x => x.ConsumedQuantity)
            .HasPrecision(18, 4);

        builder.Property(x => x.UnitOfMeasure)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasOne(x => x.WorkOrder)
            .WithMany(x => x.Materials)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}