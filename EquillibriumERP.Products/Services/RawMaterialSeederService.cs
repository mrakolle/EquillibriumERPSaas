using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Products.Interfaces;
using EquillibriumERP.Products.Domain.Entities;
using EquillibriumERP.Products.Domain.Enums;
using EquillibriumERP.Products.Infrastructure.Seeders;
using EquillibriumERP.Core.Abstractions.MultiTenancy;

namespace EquillibriumERP.Products.Services;

public sealed class RawMaterialSeederService : IRawMaterialSeeder
{
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
        
        if (!string.IsNullOrWhiteSpace(schema))
        {
            Console.WriteLine("Seeding RawMaterials During Onborading for : " + schema);
            await _db.Database.OpenConnectionAsync(ct);

            await _db.Database.ExecuteSqlRawAsync(
                $"SET search_path TO \"{schema}\", public",
                ct);
        }
        else
        {
            Console.WriteLine("Seeding RawMaterials for active tenants");
            await _tenantContextualizer.SetTenantContextAsync(_db, ct);
        }

        foreach (var material in RawMaterialCatalog.Materials)
        {
            var exists = await _db.Products
                .AnyAsync(x => x.ProductCode == material.ProductCode, ct);

            if (exists)
                continue;

            _db.Products.Add(
                new Product(
                    material.ProductCode,
                    material.ProductName,
                    ProductType.RawMaterial,
                    0m,
                    0m
                )
            );
        }

        await _db.SaveChangesAsync(ct);
    }
}