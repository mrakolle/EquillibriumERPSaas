using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Persistence.Configurations;

public class EstimateSettingsConfiguration : IEntityTypeConfiguration<EstimateSettings>
{
    public void Configure(EntityTypeBuilder<EstimateSettings> builder)
    {
        builder.ToTable("EstimateConfigurations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Prefix).IsRequired().HasMaxLength(10);
        builder.Property(x => x.IsEnabled).IsRequired();
    }
}