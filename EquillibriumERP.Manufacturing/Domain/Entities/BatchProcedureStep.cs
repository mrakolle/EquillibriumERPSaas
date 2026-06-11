using System;

namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class BatchProcedureStep
{
    public Guid Id { get; set; }

    public Guid BatchProcedureId { get; set; }

    public string StepName { get; set; } = null!;

    public int Sequence { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public decimal? ActualDurationHours { get; set; }

    public BatchProcedure BatchProcedure { get; set; } = null!;

    public void Start()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Cannot start a completed step.");

        if (StartedAt != null)
            throw new InvalidOperationException("Step already started.");

        StartedAt = DateTime.UtcNow;
    }

    public void Complete(decimal actualHours)
    {
        if (StartedAt == null)
            throw new InvalidOperationException("Step must be started before completion.");

        if (IsCompleted)
            throw new InvalidOperationException("Step already completed.");

        if (actualHours <= 0)
            throw new InvalidOperationException("Actual hours must be greater than zero.");

        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
        ActualDurationHours = actualHours;
    }
}