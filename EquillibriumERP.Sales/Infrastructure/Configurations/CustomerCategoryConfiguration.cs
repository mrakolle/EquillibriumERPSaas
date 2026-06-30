using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Persistence.Configurations;

public class CustomerCategoryConfiguration : IEntityTypeConfiguration<CustomerCategory>
{
    public void Configure(EntityTypeBuilder<CustomerCategory> builder)
    {
        builder.ToTable("CustomerCategories");

        builder.HasKey(x => x.Id);
    }
}