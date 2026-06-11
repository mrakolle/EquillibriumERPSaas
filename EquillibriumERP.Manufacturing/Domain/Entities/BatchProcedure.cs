using System;
using System.Collections.Generic;

namespace EquillibriumERP.Manufacturing.Domain.Entities;

public class BatchProcedure
{
    public Guid Id { get; set; }

    public Guid BatchId { get; set; }

    public string Name { get; set; } = null!;

    public int Sequence { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public Batch Batch { get; set; } = null!;

    public ICollection<BatchProcedureStep> Steps { get; set; }
        = new List<BatchProcedureStep>();

    public void Start()
    {
        StartedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (Steps.Any(s => !s.IsCompleted))
            throw new InvalidOperationException("All steps must be completed first.");

        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
    }
    public void StartNextStep()
    {
        var nextStep = Steps
            .OrderBy(x => x.Sequence)
            .FirstOrDefault(x => !x.IsCompleted);

        if (nextStep == null)
            throw new InvalidOperationException("No remaining steps.");

        nextStep.Start();
    }

    public void CompleteCurrentStep(decimal actualHours)
    {
        var currentStep = Steps
            .OrderBy(x => x.Sequence)
            .FirstOrDefault(x => x.StartedAt != null && !x.IsCompleted);

        if (currentStep == null)
            throw new InvalidOperationException("No active step found.");

        currentStep.Complete(actualHours);
    }
}