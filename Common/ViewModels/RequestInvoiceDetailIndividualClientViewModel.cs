using System.ComponentModel.DataAnnotations;
using Common.Dtos;

namespace Common.ViewModels;

public class RequestInvoiceDetailIndividualClientViewModel : RequestInvoiceDetailViewModel
{
    public RequestInvoiceDetailIndividualClientViewModel()
    {
    }

    public RequestInvoiceDetailIndividualClientViewModel(RequestInvoiceDetailDto model)
    {
        Name = model.Name ;
        Surname = model.Surname;
        AddressViewModel = new AddressViewModel(model.City, model.Street, model.Postcode);
    }


    [Display(Name = "Imię")]
    [Required(ErrorMessage = "Imię jest wymagane")]
    [MaxLength(50, ErrorMessage = "Maksymalna ilość znaków to 50")]
    public string Name { get; set; } = null!;

    [Display(Name = "Nazwisko")]
    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    [MaxLength(50, ErrorMessage = "Maksymalna ilość znaków to 50")]
    public string Surname { get; set; } = null!;
}