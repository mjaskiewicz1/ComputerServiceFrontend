using System.ComponentModel.DataAnnotations;
using Common.Dtos;
using Common.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.ViewModels;

public class RequestCreateViewModel : RequestViewModel
{
    public readonly string[] ClientTypes = { "Osoba prywatna", "Firma" };
    public readonly string[] ShipmentTypes = { "Wysyłka", "Odbiór osobisty" };


    public RequestCreateViewModel()
    {
    }

    public RequestCreateViewModel(RequestStatus requestStatus)
    {
        RequestStatus = requestStatus;
    }

    public RequestCreateViewModel(RequestDto model)
    {
        Url = model.Url;
        Rma = model.Rma;
        EmployeeObjectId = model.EmployeeObjectId;
        Name = model.Name;
        Surname = model.Surname;
        Surname = model.Surname;
        PhoneNumber = model.PhoneNumber;
        Email = model.Email;
        Brand = model.Brand;
        Model = model.Model;
        SerialNumber = model.SerialNumber;
        Details = model.Details;
        FailureDescription = model.FailureDescription;
        RequestStatus = model.RequestStatus;
        if (model.RequestInvoiceDetail.Nip != null)
        {
            RequestInvoiceDetailCompanyViewModel = new RequestInvoiceDetailCompanyViewModel(model.RequestInvoiceDetail);
            ClientType = "Firma";
        }

        else
        {
            RequestInvoiceDetailIndividualClientViewModel =
                new RequestInvoiceDetailIndividualClientViewModel(model.RequestInvoiceDetail);
        }

        if (model.RequestShipmentDetail != null)
            RequestShipmentDetailViewModel = new RequestShipmentDetailViewModel(model.RequestShipmentDetail);
        else
            ShipmentType = "Odbiór osobisty";
    }

    [JsonConstructor]
    public RequestCreateViewModel(string? rma, string? employeeObjectId, RequestStatus requestStatus, string name,
        string surname, string email, string phoneNumber, string brand, string model,
        string? serialNumber, string? details, string failureDescription, Guid url,
        RequestInvoiceDetailDto requestInvoiceDetail,
        RequestShipmentDetailViewModel? requestShipment)
        : base(rma, employeeObjectId, requestStatus, name, surname, email, phoneNumber)
    {
        Brand = brand;
        Model = model;
        SerialNumber = serialNumber;
        Details = details;
        FailureDescription = failureDescription;
        Url = url;


        RequestShipmentDetailViewModel = requestShipment;
        if (requestInvoiceDetail.Nip == null)
            RequestInvoiceDetailIndividualClientViewModel =
                new RequestInvoiceDetailIndividualClientViewModel(requestInvoiceDetail);
        else
            RequestInvoiceDetailCompanyViewModel = new RequestInvoiceDetailCompanyViewModel(requestInvoiceDetail);
    }

    [Display(Name = "Producent")]
    [MaxLength(150, ErrorMessage = "Maksymalna ilość znaków to  150")]
    [Required(ErrorMessage = "Producent jest wymagany")]
    public string Brand { get; set; } = null!;

    [MaxLength(150, ErrorMessage = "Maksymalna ilość znaków to  150")]
    [Required(ErrorMessage = "Model wymagany")]
    public string Model { get; set; } = null!;

    [Display(Name = "Numer seryjny")]
    [MaxLength(100, ErrorMessage = "Maksymalna ilość znaków to  100")]

    public string? SerialNumber { get; set; }

    [Display(Name = "Szczegóły")]
    [MaxLength(250, ErrorMessage = "Maksymalna ilość znaków to  250")]
    public string? Details { get; set; } //np haslo do laptopa

    [Display(Name = "Opis usterki")]
    [Required(ErrorMessage = "Opis usterki wymgany ")]
    [MaxLength(600, ErrorMessage = "Maksymalna ilość znaków to  600")]
    public string FailureDescription { get; set; } = null!;

    public Guid Url { get; set; }
    public string ClientType { get; set; } = "Osoba prywatna";
    [Required] public string ShipmentType { get; set; } = "Wysyłka";
    public RequestInvoiceDetailCompanyViewModel? RequestInvoiceDetailCompanyViewModel { get; set; }
    public RequestInvoiceDetailIndividualClientViewModel? RequestInvoiceDetailIndividualClientViewModel { get; set; }
    public RequestShipmentDetailViewModel? RequestShipmentDetailViewModel { get; set; }
    public List<IFormFile>? Files { get; set; }
}