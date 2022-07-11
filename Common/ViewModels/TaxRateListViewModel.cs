namespace Common.ViewModels;

public static class TaxRateListViewModel
{
    public static Dictionary<decimal, string> TaxRate { get; set; } = new()
    {
        { 0.23m, "23%" },
        { 0.08m, "8%" },
        { 0.05m, "5%" },
        { 0m, "0%" }
    };
}