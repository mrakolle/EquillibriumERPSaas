namespace EquillibriumERP.Sales.Domain.Entities;

public class CustomerContact
{
    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; } = default!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string? Position { get; private set; }

    public string? Email { get; private set; }

    public string? Phone { get; private set; }

    public bool IsPrimary { get; private set; }

    private CustomerContact()
    {
    }

    public CustomerContact(
        Guid customerId,
        string firstName,
        string lastName,
        string? position,
        string? email,
        string? phone,
        bool isPrimary)
    {
        Id = Guid.NewGuid();

        CustomerId = customerId;

        FirstName = firstName.Trim();

        LastName = lastName.Trim();

        Position = position?.Trim();

        Email = email?.Trim();

        Phone = phone?.Trim();

        IsPrimary = isPrimary;
    }

    public void Update(
        string firstName,
        string lastName,
        string? position,
        string? email,
        string? phone,
        bool isPrimary)
    {
        FirstName = firstName.Trim();

        LastName = lastName.Trim();

        Position = position?.Trim();

        Email = email?.Trim();

        Phone = phone?.Trim();

        IsPrimary = isPrimary;
    }
}
