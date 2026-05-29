using EquillibriumERP.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EquillibriumERP.Products.Infrastructure.Persistence.Configurations;

public class ProductConfiguration
    : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.ProductCode)
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(2000);

        builder.Property(x => x.ProductType)
            .IsRequired();

        builder.Property(x => x.SellingPrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.CostPrice)
            .HasPrecision(18, 2);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.ProductCategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}