using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Manufacturing.Infrastructure.Configurations;

public class ProductBatchConfiguration : IEntityTypeConfiguration<ProductBatch>
{
    public void Configure(EntityTypeBuilder<ProductBatch> builder)
    {
        builder.ToTable("ProductBatches");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BatchNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.QuantityProduced)
            .HasPrecision(18, 4);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.WorkOrderId);
        builder.HasIndex(x => x.BatchNumber);

        builder.HasOne(x => x.WorkOrder)
            .WithMany(x => x.ProductBatches)
            .HasForeignKey(x => x.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}