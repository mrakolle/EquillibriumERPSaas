namespace EquillibriumERP.Sales.Domain.Entities;

public class CustomerCategory
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
}


/*using EquillibriumERP.SharedKernel.Common;

namespace EquillibriumERP.Sales.Domain.Entities;

public class CustomerCategory : AuditableEntity
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public ICollection<Customer> Customers { get; set; }
        = new List<Customer>();
}*/