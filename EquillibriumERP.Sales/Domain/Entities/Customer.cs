using EquillibriumERP.Core.Abstractions.Domain.Enums;

namespace EquillibriumERP.Sales.Domain.Entities;

public class Customer
{
    public Guid Id { get; private set; }

    public string CustomerCode { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public CustomerType CustomerType { get; private set; }

    public Guid? CustomerCategoryId { get; private set; }

    public CustomerCategory? CustomerCategory { get; private set; }

    public string? RegistrationNumber { get; private set; }

    public string? VatNumber { get; private set; }

    public string? TaxNumber { get; private set; }

    public string? Email { get; private set; }

    public string? Phone { get; private set; }

    public string? Mobile { get; private set; }

    public string? Website { get; private set; }

    public decimal CreditLimit { get; private set; }

    public int PaymentTerms { get; private set; }

    public bool IsActive { get; private set; }

    public ICollection<CustomerAddress> Addresses { get; private set; } = new List<CustomerAddress>();

    public ICollection<CustomerContact> Contacts { get; private set; } = new List<CustomerContact>();

    private Customer() { }

    public Customer(
        string customerCode,
        string name,
        CustomerType customerType,
        Guid? customerCategoryId,
        string? registrationNumber,
        string? vatNumber,
        string? taxNumber,
        string? email,
        string? phone,
        string? mobile,
        string? website,
        decimal creditLimit,
        int paymentTerms)
    {
        Id = Guid.NewGuid();

        CustomerCode = customerCode;
        Name = name;
        CustomerType = customerType;
        CustomerCategoryId = customerCategoryId;

        RegistrationNumber = registrationNumber;
        VatNumber = vatNumber;
        TaxNumber = taxNumber;

        Email = email;
        Phone = phone;
        Mobile = mobile;
        Website = website;

        CreditLimit = creditLimit;
        PaymentTerms = paymentTerms;

        IsActive = true;
    }

    public void Update(
        string customerCode,
        string name,
        CustomerType customerType,
        Guid? customerCategoryId,
        string? registrationNumber,
        string? vatNumber,
        string? taxNumber,
        string? email,
        string? phone,
        string? mobile,
        string? website,
        decimal creditLimit,
        int paymentTerms)
    {
        CustomerCode = customerCode;
        Name = name;
        CustomerType = customerType;
        CustomerCategoryId = customerCategoryId;

        RegistrationNumber = registrationNumber;
        VatNumber = vatNumber;
        TaxNumber = taxNumber;

        Email = email;
        Phone = phone;
        Mobile = mobile;
        Website = website;

        CreditLimit = creditLimit;
        PaymentTerms = paymentTerms;
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}