using Newtonsoft.Json;

namespace Common.ViewModels;

public class SummaryTaxViewModel
{
    public List<SummaryTaxItemViewModel> SummaryTax;

    [JsonConstructor]
    public SummaryTaxViewModel(List<SummaryTaxItemViewModel> summaryTax)
    {
        SummaryTax = summaryTax;

        foreach (var item in summaryTax)
        {
            GrossTotal += item.GrossTotal;
            NetTotal += item.NetTotal;
            TaxTotal += item.TaxValue;
        }
    }

    public decimal TaxTotal { get; set; }
    public decimal NetTotal { get; set; }
    public decimal GrossTotal { get; set; }
}