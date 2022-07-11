using System.ComponentModel.DataAnnotations;
using Common.Helppers.ValidationAttributes;
using Newtonsoft.Json;

namespace Common.ViewModels;

public class ConfigViewModel
{
    public ConfigViewModel()
    {
    }

    [JsonConstructor]
    public ConfigViewModel(int id, string name, string nip, string phoneNumber, string email, string city,
        string street, string postcode, string bankAccountNumber, string postalTown, string bankName)
    {
        Id = id;
        Name = name;
        Nip = nip;
        PhoneNumber = phoneNumber;
        Email = email;
        BankAccountNumber = bankAccountNumber;
        PostalTown = postalTown;
        BankName = bankName;
        AddressViewModel = new AddressViewModel(city, street, postcode);
    }

    [Display(Name = "Numer Konta")]
    [Required(ErrorMessage = "Numer konta wymagany")]
    [RegularExpression("^[0-9]{26,26}$", ErrorMessage = "Niepoprawny format ")]
    public string BankAccountNumber { get; set; } = null!;

    [Display(Name = "Poczta")]
    [Required(ErrorMessage = "Poczta wymagana")]
    [MaxLength(250)]
    public string PostalTown { get; set; } = null!;

    [Display(Name = "Nazwa Banku")]
    [Required(ErrorMessage = "Nazwa banku jest wymgana")]
    [MaxLength(250,ErrorMessage = "Nazwa firmy jest za długa (maksymalna ilość znaków 250)")]
    public string BankName { get; set; } = null!;

    public int Id { get; set; }

    [Display(Name = "Nazwa Firmy")]
    [Required(ErrorMessage = "Nazwa firmy jest wymagana")]
    [MaxLength(150, ErrorMessage = "Nazwa firmy jest za długa (maksymalna ilość znaków 150) ")]
    public string Name { get; set; } = null!;

    [Nip(ErrorMessage = "Nieprawidłowy format NIP")]
    [Required(ErrorMessage = "NIP jest wymagany")]
    [RegularExpression(@"\d{3}-\d{3}-\d{2}-\d{2}", ErrorMessage = "Nieprawidłowy format NIP")]
    public string Nip { get; set; } = null!;

    [Display(Name = "Telefon")]
    [Required(ErrorMessage = "Telefon jest wymagany")]
    [MaxLength(15, ErrorMessage = "Telefon ma nieporpawny format")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = null!;

    public AddressViewModel AddressViewModel { get; set; } = null!;

    [Required(ErrorMessage = "Email jest wymagany")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email jest nieprawidłowy")]
    [MaxLength(256, ErrorMessage = "Email jest zbyt długi (maksymalna ilość znaków 256)")]
    public string Email { get; set; } = null!;
}