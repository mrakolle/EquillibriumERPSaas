namespace EquillibriumERP.Purchasing.Domain.Entities;

public class SupplierCategory
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;

    public bool IsActive { get; private set; }

    private SupplierCategory() { }

    public SupplierCategory(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        IsActive = true;
    }

    public void Update(string name)
    {
        Name = name;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}