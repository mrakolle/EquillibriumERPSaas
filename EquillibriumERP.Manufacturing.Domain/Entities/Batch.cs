using System;
using System.Collections.Generic;
using System.Linq;
using EquillibriumERP.Manufacturing.Domain.Enums;

namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class Batch
{
    public Guid Id { get; set; }

    public string BatchNumber { get; set; } = null!;

    public Guid BillOfMaterialId { get; set; }

    public decimal PlannedQuantity { get; set; }

    public decimal ProducedQuantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public BatchStatus Status { get; set; }

    public BillOfMaterial BillOfMaterial { get; set; } = null!;

    public ICollection<BatchConsumption> Consumptions { get; set; } = new List<BatchConsumption>();

    public ICollection<BatchOutput> Outputs { get; set; } = new List<BatchOutput>();
}