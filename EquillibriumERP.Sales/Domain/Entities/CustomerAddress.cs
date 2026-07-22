namespace EquillibriumERP.Sales.Domain.Entities;

public class CustomerAddress
{
    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }
    public Customer Customer { get; private set; } = default!;

    public string AddressType { get; private set; } = null!;

    public string AddressLine1 { get; private set; } = null!;

    public string? AddressLine2 { get; private set; }

    public string City { get; private set; } = null!;

    public string Province { get; private set; } = null!;

    public string PostalCode { get; private set; } = null!;

    public string Country { get; private set; } = null!;

    public bool IsPrimary { get; private set; }

    private CustomerAddress()
    {
    }

    public CustomerAddress(
        Guid customerId,
        string addressType,
        string addressLine1,
        string? addressLine2,
        string city,
        string province,
        string postalCode,
        string country,
        bool isPrimary)
    {
        Id = Guid.NewGuid();

        CustomerId = customerId;

        AddressType = addressType.Trim();

        AddressLine1 = addressLine1.Trim();

        AddressLine2 = addressLine2?.Trim();

        City = city.Trim();

        Province = province.Trim();

        PostalCode = postalCode.Trim();

        Country = country.Trim();

        IsPrimary = isPrimary;
    }

    public void Update(
        string addressType,
        string addressLine1,
        string? addressLine2,
        string city,
        string province,
        string postalCode,
        string country,
        bool isPrimary)
    {
        AddressType = addressType.Trim();

        AddressLine1 = addressLine1.Trim();

        AddressLine2 = addressLine2?.Trim();

        City = city.Trim();

        Province = province.Trim();

        PostalCode = postalCode.Trim();

        Country = country.Trim();

        IsPrimary = isPrimary;
    }
}

