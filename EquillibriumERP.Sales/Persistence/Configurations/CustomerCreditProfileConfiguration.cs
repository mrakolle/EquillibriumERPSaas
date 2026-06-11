using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Persistence.Configurations;

public class CustomerCreditProfileConfiguration : IEntityTypeConfiguration<CustomerCreditProfile>
{
    public void Configure(EntityTypeBuilder<CustomerCreditProfile> builder)
    {
        builder.ToTable("CustomerCreditProfiles");

        builder.HasKey(x => x.Id);
    }
}