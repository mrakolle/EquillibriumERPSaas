using EquillibriumERP.SharedKernel.Common;

namespace EquillibriumERP.Sales.Domain.Entities;

public class CustomerAddress : AuditableEntity
{
    public Guid CustomerId { get; set; }

    public string AddressLine1 { get; set; } = default!;

    public string? AddressLine2 { get; set; }

    public string City { get; set; } = default!;

    public string Province { get; set; } = default!;

    public string PostalCode { get; set; } = default!;

    public string Country { get; set; } = "South Africa";

    public bool IsPrimary { get; set; }

    public Customer Customer { get; set; } = default!;
}