using Common.ViewModels;

namespace Common.Dtos;

public class RequestInvoiceDetailDto
{
    public RequestInvoiceDetailDto()
    {
    }

    public RequestInvoiceDetailDto(RequestInvoiceDetailIndividualClientViewModel invoiceDetailIndividualClient)
    {
        Name = invoiceDetailIndividualClient.Name;
        Surname = invoiceDetailIndividualClient.Surname;
        City = invoiceDetailIndividualClient.AddressViewModel.City;
        Street = invoiceDetailIndividualClient.AddressViewModel.Street;
        Postcode = invoiceDetailIndividualClient.AddressViewModel.Postcode.Replace("-", string.Empty);
    }

    public RequestInvoiceDetailDto(RequestInvoiceDetailCompanyViewModel invoiceDetailCompanyViewModel)
    {
        NameCompany = invoiceDetailCompanyViewModel.NameCompany;
        Nip = invoiceDetailCompanyViewModel.Nip.Replace("-", string.Empty);
        City = invoiceDetailCompanyViewModel.AddressViewModel.City;
        Street = invoiceDetailCompanyViewModel.AddressViewModel.Street;
        Postcode = invoiceDetailCompanyViewModel.AddressViewModel.Postcode.Replace("-", string.Empty);
    }

    public RequestInvoiceDetailDto(string? nip, string nameCompany, string name, string surname, string city,
        string street, string postcode, string rma)
    {
        City = city;
        Name = name;
        NameCompany = nameCompany;
        Nip = nip;
        Postcode = postcode;
        Rma = rma;
        Street = street;
        Surname = surname;
    }

    public string? Rma { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? NameCompany { get; set; }
    public string? Nip { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Postcode { get; set; } = null!;
}