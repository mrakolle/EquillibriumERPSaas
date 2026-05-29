using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Manufacturing.Infrastructure.Persistence.Configurations;

public class BatchConfiguration : IEntityTypeConfiguration<Batch>
{
    public void Configure(EntityTypeBuilder<Batch> builder)
    {
        builder.ToTable("Batches");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.BatchNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.PlannedQuantity)
            .HasPrecision(18, 2);

        builder.Property(x => x.ProducedQuantity)
            .HasPrecision(18, 2);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasOne(x => x.BillOfMaterial)
            .WithMany()
            .HasForeignKey(x => x.BillOfMaterialId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Consumptions)
            .WithOne()
            .HasForeignKey(x => x.BatchId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Outputs)
            .WithOne()
            .HasForeignKey(x => x.BatchId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}