using Common.Enums;

namespace Common.Dtos;

public class InvoiceDto
{
    public InvoiceDto()
    {
    }

    public InvoiceDto(string city, string employeeObjectId, DateTime invoiceDate, DateTime invoicePayment,
        List<InvoiceItemDto> items, string? name, string? nameCompany, string? nip, PaymentsMethod paymentsMethod,
        PaymentStatus paymentStatus, string postcode, string rma, DateTime saleDate, string street, string? surname,
        decimal total)
    {
        City = city;
        EmployeeObjectId = employeeObjectId;
        InvoiceDate = invoiceDate;
        InvoicePayment = invoicePayment;
        Items = items;
        Name = name;
        NameCompany = nameCompany;
        Nip = nip?.Replace("-", string.Empty);
        PaymentsMethod = paymentsMethod;
        PaymentStatus = paymentStatus;
        Postcode = postcode.Replace("-", string.Empty);
        Rma = rma;
        SaleDate = saleDate;
        Street = street;
        Surname = surname;
        Total = total;
    }

    public string EmployeeObjectId { get; set; }
    public string Rma { get; set; }
    public string? Number { get; set; }
    public decimal Total { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime SaleDate { get; set; }
    public DateTime InvoicePayment { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentsMethod PaymentsMethod { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? NameCompany { get; set; }
    public string? Nip { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Postcode { get; set; } = null!;
    public List<InvoiceItemDto> Items { get; set; }
}