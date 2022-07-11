using System.ComponentModel.DataAnnotations;
using Common.Enums;
using Newtonsoft.Json;

namespace Common.ViewModels;

public class InvoiceCreateViewModel : InvoiceViewModel
{


    public InvoiceCreateViewModel()
    {
        
    }

    [JsonConstructor]
    public InvoiceCreateViewModel(string employeeObjectId, List<InvoiceItemCreteViewModel> Items, string? name,
        string? nameCompany, string? nip, string? number, PaymentsMethod paymentsMethod, string rma, string? surname,
        string city, string street, string postcode)
    {
        EmployeeObjectId = employeeObjectId;
        InvoiceItems = Items;
        Name = name;
        NameCompany = nameCompany;
        Nip = nip;
        Number = number;
        PaymentsMethod = paymentsMethod;
        Rma = rma;
        Surname = surname;
        AddressViewModel = new AddressViewModel(city, street, postcode);
        
    }

    public string EmployeeObjectId { get; set; } = null!;
    public new string Rma { get; set; }

    [Display(Name = "Numer Faktury")] public new string? Number { get; set; }


    [Display(Name = "Data sprzedarzy")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Data sprzedarzy  wymagana")]
    public DateTime SaleDate { get; set; } = DateTime.Now;

    [Display(Name = "Termin płatności")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Termin płatności wymgany")]
    public DateTime InvoicePayment { get; set; } = DateTime.Now;

    [Display(Name = "Sposób płatności")] public PaymentsMethod PaymentsMethod { get; set; } = PaymentsMethod.Gotówka;

    [Display(Name = "Nazwa firmy")]
    [MaxLength(150, ErrorMessage = "Maksymalna długość pola to 150")]
    public string? NameCompany { get; set; }

    public string? Nip { get; set; }

    [Display(Name = "Imię")]
    [Required(ErrorMessage = "Imię jest wymagane")]
    [MaxLength(50, ErrorMessage = "Maksymalna ilość znaków to 50")]
    public string? Name { get; set; }

    [Display(Name = "Nazwisko")]
    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    [MaxLength(50, ErrorMessage = "Maksymalna ilość znaków to 50")]
    public string? Surname { get; set; }
    [Display(Name = "Wycyna")]
    [DataType(DataType.Currency)]
    public decimal Estimate { get; set; }

    public RequestStatus RequestStatus { get; set; }
    public AddressViewModel AddressViewModel { get; set; }
    public IEnumerable<InvoiceItemCreteViewModel>? ListProducts { get; set; }
    public List<InvoiceItemCreteViewModel>? InvoiceItems { get; set; }

}