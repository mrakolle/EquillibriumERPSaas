namespace EquillibriumERP.Sales.Domain.Entities;

public class InvoiceSequence
{
    public Guid Id { get; set; }

    public int Year { get; set; }

    public int NextNumber { get; set; }

    public InvoiceSequence()
    {
    }

    public InvoiceSequence(int year)
    {
        Year = year;
        NextNumber = 1;
    }

    public int Next()
    {
        return NextNumber++;
    }
}


/*public class InvoiceSequence
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public long LastNumber { get; set; }

    public InvoiceSequence() { }

    public InvoiceSequence(int year)
    {
        Id = Guid.NewGuid();
        Year = year;
        LastNumber = 0;
    }

    public long Next()
    {
        LastNumber++;
        return LastNumber;
    }
}*/