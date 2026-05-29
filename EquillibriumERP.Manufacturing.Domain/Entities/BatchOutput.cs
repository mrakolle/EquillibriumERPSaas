using System;

namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class BatchOutput
{
    public Guid Id { get; set; }

    public Guid BatchId { get; set; }

    public Guid ProductId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime ProducedAt { get; set; }

    public Batch Batch { get; set; } = null!;

    public void Produce(decimal quantity)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Produced quantity must be greater than zero.");

        Quantity += quantity;
        ProducedAt = DateTime.UtcNow;
    }
}