using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels;

public class SearchInvoiceItemViewModel
{
    [Display(Name = "Szukaj produktu")]
    [Required(ErrorMessage = "Produkt wymagany")]
    public string? Code { get; set; }

    [Display(Name = "Ilość")]
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Ilość  nie może być mniejsza niż 1")]
    public int Amount { get; set; } = 1;
}