namespace Common.Dtos;

public class InvoiceItemDto
{
    public InvoiceItemDto()
    {
    }

    public InvoiceItemDto(int? amount, string basicUnit, string code, string name, decimal price, decimal tax)
    {
        Amount = amount;
        BasicUnit = basicUnit;
        Code = code;
        Name = name;
        Price = price;
        Tax = tax;
    }

    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Tax { get; set; }
    public int? Amount { get; set; }
    public string BasicUnit { get; set; } = null!;
}