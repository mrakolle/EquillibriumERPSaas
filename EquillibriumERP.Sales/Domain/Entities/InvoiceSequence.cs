public class InvoiceSequence
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
}