using EquillibriumERP.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class InvoiceSequenceConfiguration : IEntityTypeConfiguration<InvoiceSequence>
{
    public void Configure(EntityTypeBuilder<InvoiceSequence> builder)
    {
        builder.ToTable("InvoiceSequences");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Year).IsUnique();

        builder.Property(x => x.Year).IsRequired();
        builder.Property(x => x.LastNumber).IsRequired();
    }
}