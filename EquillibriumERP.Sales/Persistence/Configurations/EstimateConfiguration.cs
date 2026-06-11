using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Persistence.Configurations;

public class EstimateConfiguration : IEntityTypeConfiguration<Estimate>
{
    public void Configure(EntityTypeBuilder<Estimate> builder)
    {
        builder.ToTable("Estimates");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerId)
            .IsRequired();

        builder.Property(x => x.ReferenceNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.EstimateDateUtc)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.ExpiryDateUtc)
            .IsRequired(false);

        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.EstimateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}