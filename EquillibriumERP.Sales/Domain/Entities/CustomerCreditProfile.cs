namespace EquillibriumERP.Sales.Domain.Entities;

public class CustomerCreditProfile
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

    public decimal CreditLimit { get; set; }
}


/*using EquillibriumERP.SharedKernel.Common;

namespace EquillibriumERP.Sales.Domain.Entities;

public class CustomerCreditProfile : AuditableEntity
{
    public Guid CustomerId { get; set; }

    public decimal CreditLimit { get; set; }

    public decimal CurrentBalance { get; set; }

    public int PaymentTermsInDays { get; set; }

    public bool IsOnHold { get; set; }

    public Customer Customer { get; set; } = default!;
}*/