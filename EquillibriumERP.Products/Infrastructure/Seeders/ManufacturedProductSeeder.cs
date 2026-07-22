using Microsoft.EntityFrameworkCore;
using EquillibriumERP.Products.Contracts.Seeders;
using EquillibriumERP.Products.Domain.Entities;
using EquillibriumERP.Core.Abstractions.Domain.Enums;

namespace EquillibriumERP.Products.Infrastructure.Seeders;
public static class ManufacturedProductSeeder
{
    public static async Task SeedAsync(ProductsDbContext db, CancellationToken ct)
    {
        
        
        var categories = await db.ProductCategories
            .Where(x => x.ProductType == ProductType.Manufactured)
            .ToDictionaryAsync(
                x => x.Name,
                x => x.Id,
                cancellationToken: ct);

        var products = new List<ProductSeed>
        {
            new("ALCSAN", "Alcohol Hand Sanitizer", "Sanitizers"),
            new("NALCSAN", "Non-Alcohol Hand Sanitizer", "Sanitizers"),

            new("SURFCLEAN", "Surface Cleaner", "Household"),
            new("GLASSCLEAN", "Glass Cleaner", "Household"),
            new("FLOORCLEAN", "Floor Cleaner", "Household"),

            new("DEGREASER", "Heavy Duty Degreaser", "Industrial"),
            new("INDSAN", "Industrial Sanitizer", "Industrial"),

            new("CARSHAMPOO", "Car Shampoo", "Automotive"),
            new("TYRESHINE", "Tyre Shine", "Automotive"),

            new("FRENCHDRAIN", "French Drain Bio-Enzyme", "Bio-Enzymes"),
            new("GREASETRAP", "Grease Trap Bio-Enzyme", "Bio-Enzymes"),

            new("HANDSOAP", "Liquid Hand Soap", "Personal Care"),
            new("BODYWASH", "Body Wash", "Personal Care")
        };

        foreach (var p in products)
        {
            if (!categories.TryGetValue(p.CategoryName, out var categoryId))
                throw new Exception($"Missing category: {p.CategoryName}");

            db.Products.Add(new Product(
                p.ProductCode,
                p.Name,
                ProductType.Manufactured,
                0m,
                0m,
                categoryId
            ));
        }

        await db.SaveChangesAsync(ct);
    }
}