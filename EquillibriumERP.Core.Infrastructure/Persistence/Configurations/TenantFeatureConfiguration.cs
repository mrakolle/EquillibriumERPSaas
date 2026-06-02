using EquillibriumERP.Core.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Core.Infrastructure.Persistence.Configurations;

public class TenantFeatureConfiguration : IEntityTypeConfiguration<TenantFeature>
{
    public void Configure(EntityTypeBuilder<TenantFeature> builder)
    {
        builder.ToTable("TenantFeatures");

        builder.HasKey(x => new { x.TenantId, x.FeatureId });

        builder.Property(x => x.IsEnabled)
               .IsRequired();

        builder.HasOne(x => x.Tenant)
               .WithMany(x => x.Features)
               .HasForeignKey(x => x.TenantId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Feature)
               .WithMany()
               .HasForeignKey(x => x.FeatureId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}