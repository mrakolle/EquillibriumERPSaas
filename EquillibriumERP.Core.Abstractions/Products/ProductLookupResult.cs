namespace EquillibriumERP.Core.Abstractions.Products;

public class ProductLookupResult
{
    public Guid Id { get; set; }

    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public decimal SellingPrice { get; set; }

    public bool IsActive { get; set; }
}