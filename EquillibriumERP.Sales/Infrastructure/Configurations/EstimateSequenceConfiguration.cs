using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Sales.Persistence.Configurations;

public class EstimateSequenceConfiguration : IEntityTypeConfiguration<EstimateSequence>
{
    public void Configure(EntityTypeBuilder<EstimateSequence> builder)
    {
        builder.ToTable("EstimateSequences");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.NextNumber)
            .IsRequired();
    }
}