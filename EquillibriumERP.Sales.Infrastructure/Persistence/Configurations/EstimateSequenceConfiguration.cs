using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Infrastructure.Persistence.Configurations;

public class EstimateSequenceConfiguration : IEntityTypeConfiguration<EstimateSequence>
{
    public void Configure(EntityTypeBuilder<EstimateSequence> builder)
    {
        builder.ToTable("EstimateSequences");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Year).IsRequired();

        builder.Property(x => x.LastNumber).IsRequired();

        builder.HasIndex(x => x.Year).IsUnique(); // IMPORTANT for per-year sequence
    }
}