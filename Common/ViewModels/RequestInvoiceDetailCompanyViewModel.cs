using System.ComponentModel.DataAnnotations;
using Common.Dtos;
using Common.Helppers.ValidationAttributes;

namespace Common.ViewModels;

public class RequestInvoiceDetailCompanyViewModel : RequestInvoiceDetailViewModel
{
    public RequestInvoiceDetailCompanyViewModel()
    {
    }

    public RequestInvoiceDetailCompanyViewModel(RequestInvoiceDetailDto model)
    {
        NameCompany = model.NameCompany;
        Nip = model.Nip;
        AddressViewModel = new AddressViewModel(model.City, model.Street, model.Postcode);
    }

    [Display(Name = "Nazwa firmy")]
    [MaxLength(150, ErrorMessage = "Maksymalna długość pola to 150")]
    public string NameCompany { get; set; } = null!;

    [Nip(ErrorMessage = "Nieprawidłowy format NIP")]
    [Required(ErrorMessage = "NIP jest wymagany")]
    public string Nip { get; set; } = null!;
}