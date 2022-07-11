namespace Common.ViewModels;

public class SummaryTaxItemViewModel
{
    public decimal Tax { get; set; }
    public decimal NetTotal { get; set; }
    public decimal GrossTotal { get; set; }
    public decimal TaxValue => GrossTotal - NetTotal;
}