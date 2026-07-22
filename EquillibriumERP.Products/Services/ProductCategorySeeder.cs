using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Core.Abstractions.MultiTenancy;
using EquillibriumERP.Core.Abstractions.Domain.Enums;
using EquillibriumERP.Products.Domain.Entities;

namespace EquillibriumERP.Products.Services;
public sealed class ProductCategorySeeder : ITenantModuleSeeder
{
    public int Order => 10;

    public string Name => "Product Categories";

    private readonly ProductsDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    public ProductCategorySeeder(
        ProductsDbContext db,
        ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
    }

    public async Task SeedAsync(
    string? schema = null,
    CancellationToken ct = default)
    {
        
        if (!string.IsNullOrWhiteSpace(schema))
        {
            await _tenantContextualizer.SetProvisioningTenantContextAsync(
                _db,
                schema,
                ct);
        }
        else
        {
            await _tenantContextualizer.SetTenantContextAsync(
                _db,
                ct);
        }

        if (await _db.ProductCategories.AnyAsync(ct))
            return;

            _db.ProductCategories.AddRange(
                new ProductCategory("Acids", ProductType.RawMaterial),
                new ProductCategory("Additives", ProductType.RawMaterial),
                new ProductCategory("Alcohols", ProductType.RawMaterial),
                new ProductCategory("Alkalis", ProductType.RawMaterial),
                new ProductCategory("Bio", ProductType.RawMaterial),
                new ProductCategory("Builders", ProductType.RawMaterial),
                new ProductCategory("Colourants", ProductType.RawMaterial),
                new ProductCategory("Disinfectants", ProductType.RawMaterial),
                new ProductCategory("Enzymes", ProductType.RawMaterial),
                new ProductCategory("Glycols", ProductType.RawMaterial),
                new ProductCategory("Neutralisers", ProductType.RawMaterial),
                new ProductCategory("Preservatives", ProductType.RawMaterial),
                new ProductCategory("Soap Base", ProductType.RawMaterial),
                new ProductCategory("Solvents", ProductType.RawMaterial),
                new ProductCategory("Surfactants", ProductType.RawMaterial),
                new ProductCategory("Thickeners", ProductType.RawMaterial),
                new ProductCategory("Utilities", ProductType.RawMaterial)
                            );
            //await _tenantContextualizer.SetProvisioningTenantContextAsync(_db,schema!,ct);
            await _db.SaveChangesAsync(ct);
        }
}