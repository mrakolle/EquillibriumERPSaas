namespace EquillibriumERP.Products.Contracts.Seeders;

public sealed record RawMaterialSeed(
    string ProductCode,
    string Name,
    string Category,
    string CasNumber);