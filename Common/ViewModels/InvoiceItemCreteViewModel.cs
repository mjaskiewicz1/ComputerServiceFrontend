using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels;

public class InvoiceItemCreteViewModel
{
    public string Code { get; set; }

    [Display(Name = "Nazwa")] public string Name { get; set; }

    [Display(Name = "Cenna Netto")] public decimal Price { get; set; }


    [Display(Name = "Stawka Vat")] public decimal Tax { get; set; }

    [Display(Name = "Ilość")]
    [RegularExpression("\\d+", ErrorMessage = "Ilość nie może być mniejsza od 0")]

    public string BasicUnit { get; set; } = null!;

    public int? Amount { get; set; }

    [Display(Name = "Wartoś brutto")] public decimal GrossValue => Price + Tax * NetValue;

    public decimal NetValue
    {
        get
        {
            if (Amount != null) return Amount.Value * Price;

            return 0m;
        }
    }
}