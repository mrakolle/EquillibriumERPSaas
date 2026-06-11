using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Persistence.Configurations;

public class InvoiceSettingsConfiguration : IEntityTypeConfiguration<InvoiceSettings>
{
    public void Configure(EntityTypeBuilder<InvoiceSettings> builder)
    {
        builder.ToTable("InvoiceConfigurations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Prefix).IsRequired().HasMaxLength(10);
        builder.Property(x => x.IsEnabled).IsRequired();
    }
}