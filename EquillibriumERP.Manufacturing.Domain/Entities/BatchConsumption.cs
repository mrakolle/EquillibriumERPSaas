using System;

namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class BatchConsumption
{
    public Guid Id { get; set; }

    public Guid BatchId { get; set; }

    public Guid RawMaterialId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime ConsumedAt { get; set; }
}