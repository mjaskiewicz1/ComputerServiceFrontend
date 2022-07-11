namespace Common.Dtos;

public class InvoiceNumberDto
{
    public InvoiceNumberDto(string number)
    {
        Number = number;
    }

    public InvoiceNumberDto()
    {
    }

    public string? Number { get; set; }
    public string? Error { get; set; }
}