using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Manufacturing.Infrastructure.Configurations;

public class MaterialConsumptionConfiguration
    : IEntityTypeConfiguration<MaterialConsumption>
{
    public void Configure(EntityTypeBuilder<MaterialConsumption> builder)
    {
        builder.ToTable("MaterialConsumptions");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.LotNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasPrecision(18, 4);

        builder.Property(x => x.UnitOfMeasure)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.WorkOrderId);

        builder.HasIndex(x => x.RawMaterialProductId);

        builder.HasIndex(x => x.LotNumber);

        builder.HasOne(x => x.WorkOrder)
            .WithMany(x => x.MaterialConsumptions)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}