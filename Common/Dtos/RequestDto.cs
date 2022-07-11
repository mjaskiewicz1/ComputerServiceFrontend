using Common.Enums;
using Common.ViewModels;

namespace Common.Dtos;

public class RequestDto : RmaDto
{
    public RequestDto()
    {
    }

    public RequestDto(RequestCreateViewModel requestCreateViewModel)
    {
        Url = requestCreateViewModel.Url;
        Rma = requestCreateViewModel.Rma;
        EmployeeObjectId = requestCreateViewModel.EmployeeObjectId;
        Name = requestCreateViewModel.Name;
        Surname = requestCreateViewModel.Surname;
        PhoneNumber = requestCreateViewModel.PhoneNumber;
        Email = requestCreateViewModel.Email;
        RequestStatus = requestCreateViewModel.RequestStatus;

        Brand = requestCreateViewModel.Brand;
        Model = requestCreateViewModel.Model;
        SerialNumber = requestCreateViewModel.SerialNumber;
        Details = requestCreateViewModel.Details;
        FailureDescription = requestCreateViewModel.FailureDescription;
        if (requestCreateViewModel.ClientType == "Firma")
            RequestInvoiceDetail =
                new RequestInvoiceDetailDto(requestCreateViewModel.RequestInvoiceDetailCompanyViewModel);
        else
            RequestInvoiceDetail =
                new RequestInvoiceDetailDto(requestCreateViewModel.RequestInvoiceDetailIndividualClientViewModel);

        if (requestCreateViewModel.ShipmentType == "Wysyłka")
            RequestShipmentDetail =
                new RequestShipmentDetailDto(requestCreateViewModel.RequestShipmentDetailViewModel!);
    }

    public Guid Url { get; set; }
    public string? EmployeeObjectId { get; set; }

    //Dane Klienta
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;

    public RequestStatus RequestStatus { get; set; }

    //dane o urzadzeniu 
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string? SerialNumber { get; set; }

    public string? Details { get; set; } //np haslo do laptopa
    public string FailureDescription { get; set; } = null!;
    public RequestInvoiceDetailDto? RequestInvoiceDetail { get; set; }
    public RequestShipmentDetailDto? RequestShipmentDetail { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public List<RequestConversationDto> RequestConversations { get; set; }
}