namespace Common.Helppers;

public class InvoiceAfSettings
{
    public string Url { get; set; } = null!;
    public string? SearchProductService { get; set; }
    public string? GetRequestInvoiceDetails { get; set; }
    public string? GetInvoiceItem { get; set; }
    public string? GetAvailableProductAndService { get; set; }
}