using EquillibriumERP.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Inventory.Infrastructure.Configurations;

public sealed class InventoryItemConfiguration
    : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.ToTable("InventoryItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.QuantityOnHand)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.QuantityReserved)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ReorderLevel)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasIndex(x => x.ProductId)
            .IsUnique();
    }
}