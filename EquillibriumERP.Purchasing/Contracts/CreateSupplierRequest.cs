namespace EquillibriumERP.Purchasing.Contracts;

public class CreateSupplierRequest
{
    public string Name { get; set; } = string.Empty;

    public Guid? SupplierCategoryId { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? VatNumber { get; set; }

    public string? TaxNumber { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Website { get; set; }

    public int PaymentTerms { get; set; }
}