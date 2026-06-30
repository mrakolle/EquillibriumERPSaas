using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Persistence.Configurations;

public class EstimateItemConfiguration : IEntityTypeConfiguration<EstimateItem>
{
    public void Configure(EntityTypeBuilder<EstimateItem> builder)
    {
        builder.ToTable("EstimateItems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .HasColumnType("decimal(18,2)");
    }
}