using EquillibriumERP.Core.Abstractions.Domain.Enums;

namespace EquillibriumERP.Purchasing.Domain.Entities;

public class SupplierAddress
{
    public Guid Id { get; private set; }

    public Guid SupplierId { get; private set; }

    public Supplier Supplier { get; private set; } = null!;

    public AddressType AddressType { get; private set; }

    public string AddressLine1 { get; private set; } = null!;

    public string? AddressLine2 { get; private set; }

    public string? Suburb { get; private set; }

    public string City { get; private set; } = null!;

    public string? StateOrProvince { get; private set; }

    public string? PostalCode { get; private set; }

    public string Country { get; private set; } = null!;

    public bool IsDefault { get; private set; }

    private SupplierAddress() { }

    public SupplierAddress(
        Guid supplierId,
        AddressType addressType,
        string addressLine1,
        string? addressLine2,
        string? suburb,
        string city,
        string? stateOrProvince,
        string? postalCode,
        string country,
        bool isDefault)
    {
        Id = Guid.NewGuid();

        SupplierId = supplierId;
        AddressType = addressType;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        Suburb = suburb;
        City = city;
        StateOrProvince = stateOrProvince;
        PostalCode = postalCode;
        Country = country;
        IsDefault = isDefault;
    }

    public void Update(
        AddressType addressType,
        string addressLine1,
        string? addressLine2,
        string? suburb,
        string city,
        string? stateOrProvince,
        string? postalCode,
        string country,
        bool isDefault)
    {
        AddressType = addressType;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        Suburb = suburb;
        City = city;
        StateOrProvince = stateOrProvince;
        PostalCode = postalCode;
        Country = country;
        IsDefault = isDefault;
    }
}