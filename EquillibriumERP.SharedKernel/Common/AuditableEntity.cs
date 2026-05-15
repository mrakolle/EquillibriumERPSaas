namespace EquillibriumERP.SharedKernel.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; }
}
