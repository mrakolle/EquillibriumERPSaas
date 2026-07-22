namespace EquillibriumERP.Purchasing.Domain.Entities;

public class SupplierContact
{
    public Guid Id { get; private set; }

    public Guid SupplierId { get; private set; }

    public Supplier Supplier { get; private set; } = null!;

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string? Position { get; private set; }

    public string? Email { get; private set; }

    public string? Phone { get; private set; }

    public string? Mobile { get; private set; }

    public bool IsPrimary { get; private set; }

    public bool IsActive { get; private set; }

    private SupplierContact() { }

    public SupplierContact(
        Guid supplierId,
        string firstName,
        string lastName,
        string? position,
        string? email,
        string? phone,
        string? mobile,
        bool isPrimary)
    {
        Id = Guid.NewGuid();

        SupplierId = supplierId;
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        Email = email;
        Phone = phone;
        Mobile = mobile;
        IsPrimary = isPrimary;
        IsActive = true;
    }

    public void Update(
        string firstName,
        string lastName,
        string? position,
        string? email,
        string? phone,
        string? mobile,
        bool isPrimary)
    {
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        Email = email;
        Phone = phone;
        Mobile = mobile;
        IsPrimary = isPrimary;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}