using EquillibriumERP.Purchasing.Domain.Entities;

namespace EquillibriumERP.Purchasing.Domain.Entities;

public class Supplier
{
    public Guid Id { get; private set; }

    public string SupplierCode { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public Guid? SupplierCategoryId { get; private set; }

    public SupplierCategory? SupplierCategory { get; private set; }

    public string? RegistrationNumber { get; private set; }

    public string? VatNumber { get; private set; }

    public string? TaxNumber { get; private set; }

    public string? Email { get; private set; }

    public string? Phone { get; private set; }

    public string? Mobile { get; private set; }

    public string? Website { get; private set; }

    public int PaymentTerms { get; private set; }

    public bool IsActive { get; private set; }

    public ICollection<SupplierAddress> Addresses { get; private set; } = new List<SupplierAddress>();

    public ICollection<SupplierContact> Contacts { get; private set; } = new List<SupplierContact>();

    private Supplier() { }

    public Supplier(
        string supplierCode,
        string name,
        Guid? supplierCategoryId,
        string? registrationNumber,
        string? vatNumber,
        string? taxNumber,
        string? email,
        string? phone,
        string? mobile,
        string? website,
        int paymentTerms)
    {
        Id = Guid.NewGuid();

        SupplierCode = supplierCode;
        Name = name;
        SupplierCategoryId = supplierCategoryId;

        RegistrationNumber = registrationNumber;
        VatNumber = vatNumber;
        TaxNumber = taxNumber;

        Email = email;
        Phone = phone;
        Mobile = mobile;
        Website = website;

        PaymentTerms = paymentTerms;

        IsActive = true;
    }

    public void Update(
        string supplierCode,
        string name,
        Guid? supplierCategoryId,
        string? registrationNumber,
        string? vatNumber,
        string? taxNumber,
        string? email,
        string? phone,
        string? mobile,
        string? website,
        int paymentTerms)
    {
        SupplierCode = supplierCode;
        Name = name;
        SupplierCategoryId = supplierCategoryId;

        RegistrationNumber = registrationNumber;
        VatNumber = vatNumber;
        TaxNumber = taxNumber;

        Email = email;
        Phone = phone;
        Mobile = mobile;
        Website = website;

        PaymentTerms = paymentTerms;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}