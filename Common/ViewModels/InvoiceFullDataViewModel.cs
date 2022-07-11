using Common.Enums;
using Common.Helppers;
using Newtonsoft.Json;

namespace Common.ViewModels;

public class InvoiceFullDataViewModel : InvoiceCreateViewModel
{
    public SummaryTaxViewModel SummaryTax;

    [JsonConstructor]
    public InvoiceFullDataViewModel(string employeeObjectId, List<InvoiceItemCreteViewModel> Items, string? name,
        string? nameCompany, string? nip, string? number, PaymentsMethod paymentsMethod, string rma, string? surname,
        string city, string street, string postcode, List<SummaryTaxItemViewModel> summaryTax, string clientEmail) :
        base(employeeObjectId, Items, name, nameCompany, nip, number, paymentsMethod, rma, surname, city, street,
            postcode)
    {
        ClientEmail = clientEmail;
        SummaryTax = new SummaryTaxViewModel(summaryTax);
    }


    public ConfigViewModel Config { get; set; }
    public string ClientEmail { get; set; }
    public string FullName => $"{Name} {Surname}";
    public string TotalInWords => NumberToTextHelper.TotalInWords(Total);
}