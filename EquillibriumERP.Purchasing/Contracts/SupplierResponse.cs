namespace EquillibriumERP.Purchasing.Contracts;

public class SupplierResponse
{
    public Guid Id { get; set; }
    public string SupplierCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}