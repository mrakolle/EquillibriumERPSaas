using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Infrastructure.Persistence.Configurations;

public class CustomerCreditProfileConfiguration
    : IEntityTypeConfiguration<CustomerCreditProfile>
{
    public void Configure(EntityTypeBuilder<CustomerCreditProfile> builder)
    {
        builder.ToTable("CustomerCreditProfiles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreditLimit)
            .HasPrecision(18, 2);

        builder.Property(x => x.CurrentBalance)
            .HasPrecision(18, 2);
    }
}