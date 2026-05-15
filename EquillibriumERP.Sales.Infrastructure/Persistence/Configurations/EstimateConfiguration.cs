using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Infrastructure.Persistence.Configurations;

public class EstimateConfiguration : IEntityTypeConfiguration<Estimate>
{
    public void Configure(EntityTypeBuilder<Estimate> builder)
    {
        builder.ToTable("Estimates");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EstimateNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.EstimateNumber)
            .IsUnique();

        builder.Property(x => x.CustomerId)
            .IsRequired();

        builder.Property(x => x.EstimateDateUtc)
            .IsRequired();

        builder.Property(x => x.ExpiryDateUtc)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Subtotal)
            .HasPrecision(18, 2);

        builder.Property(x => x.TaxAmount)
            .HasPrecision(18, 2);

        builder.Property(x => x.TotalAmount)
            .HasPrecision(18, 2);

        builder.Property(x => x.Notes)
            .HasMaxLength(2000);

        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.EstimateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}