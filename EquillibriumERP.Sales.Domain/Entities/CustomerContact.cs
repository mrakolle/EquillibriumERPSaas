using EquillibriumERP.SharedKernel.Common;

namespace EquillibriumERP.Sales.Domain.Entities;

public class CustomerContact : AuditableEntity
{
    public Guid CustomerId { get; set; }

    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public string? Position { get; set; }

    public bool IsPrimary { get; set; }

    public Customer Customer { get; set; } = default!;
}