using EquillibriumERP.Purchasing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Purchasing.Infrastructure.Configurations;

public class GoodsReceiptLineConfiguration : IEntityTypeConfiguration<GoodsReceiptLine>
{
    public void Configure(EntityTypeBuilder<GoodsReceiptLine> builder)
    {
        builder.ToTable("GoodsReceiptLines");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.QuantityReceived)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.SupplierLotNo)
            .HasMaxLength(100);

        builder.HasOne(x => x.GoodsReceipt)
            .WithMany(x => x.Lines)
            .HasForeignKey(x => x.GoodsReceiptId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}