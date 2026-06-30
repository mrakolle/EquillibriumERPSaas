namespace EquillibriumERP.Manufacturing.Contracts;
public class ExecuteStepRequest
{
    public decimal ActualQuantity { get; set; }
    public string? RawMaterialLotNo { get; set; }
    public string? Comment { get; set; }

}