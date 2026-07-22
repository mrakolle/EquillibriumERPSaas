namespace EquillibriumERP.Products.Contracts.Seeders;
public sealed record ProductSeed(
    string ProductCode,
    string Name,
    string CategoryName,
    string? CasNumber = null);