using EquillibriumERP.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Inventory.Infrastructure.Configurations;

public sealed class InventoryTransactionConfiguration
    : IEntityTypeConfiguration<InventoryTransaction>
{
    public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
    {
        builder.ToTable("InventoryTransactions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.TransactionType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ReferenceType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.ReferenceId)
            .IsRequired();

        builder.Property(x => x.LotNo)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.TransactionDateUtc)
            .IsRequired();

        builder.HasIndex(x => x.ProductId);

        builder.HasIndex(x => x.ReferenceId);

        // Optional but useful later for tracing batches
        builder.HasIndex(x => x.LotNo);
    }
}