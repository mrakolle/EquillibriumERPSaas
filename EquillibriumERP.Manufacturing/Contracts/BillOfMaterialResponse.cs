public sealed record BillOfMaterialResponse(
    Guid Id,
    string Code,
    Guid ProductId,
    string ProductName,
    int Version,
    bool IsActive
);