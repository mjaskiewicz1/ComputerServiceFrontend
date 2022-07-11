using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Common.ViewModels;

public class ProductViewModel
{
    [Display(Name = "Producent")]
    [Required(ErrorMessage = "Producent jest wymagany")]
    [MaxLength(150, ErrorMessage = "Maksymalna liczba znaków 150")]
    public string Brand { get; set; } = null!;

    [MaxLength(150, ErrorMessage = "Maksymalna liczba znaków 150")]
    [Required(ErrorMessage = "Model jest wymagany")]
    public string? Model { get; set; }

    [Display(Name = "Opis")]
    [Required(ErrorMessage = "Opis jest wymagany")]
    [MaxLength(700, ErrorMessage = "Maksymalna liczba znaków 700")]
    public string? Description { get; set; }

    [Display(Name = "Jednostka")] public BasicUnitProduct BasicUnit { get; set; }


    [Display(Name = "Nazwa")] public string Name => $"{Model} {Brand}";

    [Display(Name = "Kod")] public string? Code { get; set; }

    [Display(Name = "Cena Netto")]
    [DataType(DataType.Currency, ErrorMessage = "Nieprawidłowa wartość")]
    [Required(ErrorMessage = "Cena Netto jest wymagana")]
    public decimal Price { get; set; }
    [Display(Name = "Cena Brutto")]
    [DataType(DataType.Currency)]
    public decimal GrossValue => Price *(Tax+1);
    [Required(ErrorMessage = "Stawka vat jest wymagana")]
    [Display(Name = "Stawka vat")]
    public decimal Tax { get; set; }

    [Required(ErrorMessage = "Ilość jest wymagana")]
    [Display(Name = "Ilość")]
    [RegularExpression("\\d+", ErrorMessage = "Ilość nie może być mniejsza od 0")]
    public int InStock { get; set; }
    public DateTime CreatedDateTime { get; set; }
}