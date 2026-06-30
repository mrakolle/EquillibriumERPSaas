using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Manufacturing.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquillibriumERP.Manufacturing.Infrastructure.Configurations;

public class WorkOrderTransactionConfiguration : IEntityTypeConfiguration<WorkOrderTransaction>
{
    public void Configure(EntityTypeBuilder<WorkOrderTransaction> builder)
    {
        builder.ToTable("WorkOrderTransactions");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExpectedQuantity)
            .HasPrecision(18, 4);

        builder.Property(x => x.ActualQuantity)
            .HasPrecision(18, 4);

        builder.Property(x => x.UnitOfMeasure)
            .HasMaxLength(20);

        builder.Property(x => x.ExecutedAt)
            .IsRequired();

        builder.Property(x => x.ExecutedByUserId)
            .IsRequired();
    }
}