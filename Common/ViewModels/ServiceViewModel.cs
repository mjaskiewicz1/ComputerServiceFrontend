using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Common.ViewModels;

public class ServiceViewModel
{
    [Display(Name = "Nazwa")]
    [Required(ErrorMessage = "Nazwa usługi wymagana")]
    [MaxLength(150, ErrorMessage = "Nazwa usługi wymagana")]
    public string Name { get; set; } = null!;

    [Display(Name = "Kod")] public string? Code { get; set; }

    [Display(Name = "Cena Netto")]
    [DataType(DataType.Currency,ErrorMessage = "Nieprawidłowa wartość")]
    [Required(ErrorMessage = "Cena Netto jest wymagana")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Stawka vat jest wymagana")]
    [Display(Name = "Stawka vat")]

    public decimal Tax { get; set; }
    [Display(Name = "Cena Brutto")]
    [DataType(DataType.Currency)]
    public decimal GrossValue => Price * (Tax + 1);

    [Display(Name = "Opis")]
    [MaxLength(200, ErrorMessage = "Maksymalna liczba znaków 200")]
    public string? Description { get; set; }

    [Display(Name = "Jednostka")] public BasicUnitService BasicUnit { get; set; }
    public DateTime CreatedDateTime { get; set; }
}