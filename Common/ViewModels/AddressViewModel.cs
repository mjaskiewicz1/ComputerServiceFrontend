using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Common.ViewModels;

public class AddressViewModel
{
    public AddressViewModel()
    {
    }

    [JsonConstructor]
    public AddressViewModel(string city, string street, string postcode)
    {
        City = city;
        Street = street;
        Postcode = postcode;
        PostcodeWithDash = Postcode.Insert(2, "-");
    }

    [Display(Name = "Miejscowość")]
    [Required(ErrorMessage = "Miejscowość jest wymagana")]
    [MaxLength(189, ErrorMessage = "Nazwa miejscowości jest zbyt długa (maksymalna ilość znaków 189)")]
    public string City { get; set; } = null!;

    [Display(Name = "Kod Pocztowy")]
    [Required(ErrorMessage = "Kod Pocztowy jest wymagany")]
    [RegularExpression(@"\d{2}-\d{3}", ErrorMessage = "Nieprawidłowy format kodu pocztowego")]

    public string Postcode { get; set; } = null!;

    [Display(Name = "Ulica i numer domu")]
    [Required(ErrorMessage = "Ulica  jest wymgana")]
    [MaxLength(200, ErrorMessage = "Ulica jest zbyt długa (maksymalna ilość znaków 200)")]
    public string Street { get; set; } = null!;

    [Display(Name = "Kod pocztowy")] public string? PostcodeWithDash { get; set; }
}