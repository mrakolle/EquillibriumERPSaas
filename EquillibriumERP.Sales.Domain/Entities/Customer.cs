using EquillibriumERP.SharedKernel.Common;

namespace EquillibriumERP.Sales.Domain.Entities;

public class Customer : AuditableEntity
{
    public string CustomerCode { get; set; } = default!;

    public string Name { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public bool IsActive { get; set; } = true;

    public Guid? CustomerCategoryId { get; set; }

    public CustomerCategory? CustomerCategory { get; set; }

    public CustomerCreditProfile? CreditProfile { get; set; }

    public ICollection<CustomerAddress> Addresses { get; set; }
        = new List<CustomerAddress>();

    public ICollection<CustomerContact> Contacts { get; set; }
        = new List<CustomerContact>();
}