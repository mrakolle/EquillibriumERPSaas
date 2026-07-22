namespace EquillibriumERP.Sales.Domain.Entities;

public class CustomerCategory
{
    public Guid Id { get; private set; }

    public string Code { get; private set; } = default!;

    public string Name { get; private set; } = default!;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public int SortOrder { get; private set; }

    public ICollection<Customer> Customers { get; private set; }
        = new List<Customer>();

    private CustomerCategory() { }

    public CustomerCategory(
        string code,
        string name,
        string? description,
        int sortOrder = 0)
    {
        Id = Guid.NewGuid();

        SetCode(code);

        SetName(name);

        Description = description?.Trim();

        SortOrder = sortOrder;

        IsActive = true;
    }

    public void Update(
        string code,
        string name,
        string? description,
        int sortOrder)
    {
        SetCode(code);

        SetName(name);

        Description = description?.Trim();

        SortOrder = sortOrder;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    private void SetCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException("Category code is required.");

        Code = code.Trim().ToUpperInvariant();
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name is required.");

        Name = name.Trim();
    }
}