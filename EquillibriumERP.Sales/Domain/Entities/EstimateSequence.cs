namespace EquillibriumERP.Sales.Domain.Entities;

public class EstimateSequence
{
    public Guid Id { get; private set; }

    public int Year { get; private set; }

    public long LastNumber { get; private set; }

    private EstimateSequence() { }

    public EstimateSequence(int year)
    {
        Id = Guid.NewGuid();
        Year = year;
        LastNumber = 0;
    }

    public long Next() => ++LastNumber;
}