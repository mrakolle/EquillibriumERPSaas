using EquillibriumERP.Products.Domain.Enums;
namespace EquillibriumERP.Products.Contracts;
public class CreateProductRequest
{
    public string Name { get; set; } = null!;
    public string ProductCode { get; set; } = null!;
    public ProductType ProductType { get; set; }
    public decimal SellingPrice { get; set; }
    public bool IsActive { get; set; } = true;
}