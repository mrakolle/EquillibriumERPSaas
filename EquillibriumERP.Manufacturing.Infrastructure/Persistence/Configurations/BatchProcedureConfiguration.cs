using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Manufacturing.Infrastructure.Persistence.Configurations;

public class BatchProcedureConfiguration : IEntityTypeConfiguration<BatchProcedure>
{
    public void Configure(EntityTypeBuilder<BatchProcedure> builder)
    {
        builder.ToTable("BatchProcedures");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.Steps)
            .WithOne(x => x.BatchProcedure)
            .HasForeignKey(x => x.BatchProcedureId);
    }
}