namespace EquillibriumERP.Purchasing.Domain.Entities;

public class Supplier
{
    public Guid Id { get; set; }

    public string SupplierCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? ContactPerson { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;

}