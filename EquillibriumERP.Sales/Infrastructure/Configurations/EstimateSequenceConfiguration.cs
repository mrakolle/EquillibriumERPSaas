using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Infrastructure.Persistence.Configurations;

public sealed class EstimateSequenceConfiguration : IEntityTypeConfiguration<EstimateSequence>
{
    public void Configure(EntityTypeBuilder<EstimateSequence> builder)
    {
        builder.ToTable("EstimateSequences");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Year)
            .IsUnique();
    }
}