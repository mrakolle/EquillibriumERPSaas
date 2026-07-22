namespace EquillibriumERP.Sales.Contracts;

public sealed class CustomerContactDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? Position { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool IsPrimary { get; set; }
}