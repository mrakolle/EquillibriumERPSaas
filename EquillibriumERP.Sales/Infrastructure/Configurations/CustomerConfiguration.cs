using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Infrastructure.Persistence.Configurations;

public sealed class CustomerConfiguration
    : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CustomerCode)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.CustomerCode)
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.CustomerType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.RegistrationNumber)
            .HasMaxLength(100);

        builder.Property(x => x.VatNumber)
            .HasMaxLength(100);

        builder.Property(x => x.TaxNumber)
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .HasMaxLength(256);

        builder.Property(x => x.Phone)
            .HasMaxLength(50);

        builder.Property(x => x.Mobile)
            .HasMaxLength(50);

        builder.Property(x => x.Website)
            .HasMaxLength(250);

        builder.Property(x => x.CreditLimit)
            .HasPrecision(18, 2);

        builder.Property(x => x.PaymentTerms)
            .HasDefaultValue(0);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasOne(x => x.CustomerCategory)
            .WithMany(x => x.Customers)
            .HasForeignKey(x => x.CustomerCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Addresses)
            .WithOne()
            .HasForeignKey("CustomerId");

        builder.HasMany(x => x.Contacts)
            .WithOne()
            .HasForeignKey("CustomerId");
    }
}