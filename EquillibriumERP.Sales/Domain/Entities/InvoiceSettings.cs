namespace EquillibriumERP.Sales.Domain.Entities;

public class InvoiceSettings
{
    public Guid Id { get; set; }

    public bool IsEnabled { get; set; }

    public string Prefix { get; set; } = "INV";
}