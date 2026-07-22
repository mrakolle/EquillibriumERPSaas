using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Products.Interfaces;
using EquillibriumERP.Products.Domain.Entities;
using EquillibriumERP.Core.Abstractions.Domain.Enums;
using EquillibriumERP.Products.Infrastructure.Seeders;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Products.Services;

public sealed class RawMaterialSeederService : ITenantModuleSeeder
{
    public int Order => 30;

    public string Name => "Raw Materials";

    private readonly ProductsDbContext _db;
    private readonly ITenantContextualizer _tenantContextualizer;

    public RawMaterialSeederService(
        ProductsDbContext db,
        ITenantContextualizer tenantContextualizer)
    {
        _db = db;
        _tenantContextualizer = tenantContextualizer;
    }

   public async Task SeedAsync(
    string? schema,
    CancellationToken ct)
    {
        //Set TenantContext
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

        // Load all Raw Material categories once
        var categories = await _db.ProductCategories
            .Where(x => x.ProductType == ProductType.RawMaterial)
            .ToDictionaryAsync(x => x.Name, x => x.Id, ct);

        foreach (var material in RawMaterialCatalog.Materials)
        {
            if (await _db.Products.AnyAsync(x => x.ProductCode == material.ProductCode, ct))
                continue;

            if (!categories.TryGetValue(material.Category, out var categoryId))
                throw new InvalidOperationException(
                    $"Raw Material category '{material.Category}' does not exist.");

            _db.Products.Add(
                new Product(
                    productCode: material.ProductCode,
                    name: material.Name,
                    productType: ProductType.RawMaterial,
                    sellingPrice: 0m,
                    costPrice: 0m,
                    productCategoryId: categoryId,
                    casNumber: material.CasNumber
                ));
        }

        await _db.SaveChangesAsync(ct);
    }
}