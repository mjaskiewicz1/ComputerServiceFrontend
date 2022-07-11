using System.ComponentModel.DataAnnotations;
using Common.Enums;
using Newtonsoft.Json;

namespace Common.ViewModels;

public class RequestViewModel
{
    public RequestViewModel()
    {
    }

    [JsonConstructor]
    public RequestViewModel(string? rma, string? employeeObjectId, RequestStatus requestStatus, string name,
        string surname, string email, string phoneNumber)
    {
        Rma = rma;
        EmployeeObjectId = employeeObjectId;
        RequestStatus = requestStatus;
        Name = name;
        Surname = surname;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public string? Rma { get; set; }
    public string? EmployeeObjectId { get; set; }

    [Display(Name = "Status")] public RequestStatus RequestStatus { get; set; }

    [Display(Name = "Imię")]
    [Required(ErrorMessage = "Imię jest wymagane")]
    [MaxLength(50, ErrorMessage = "Maksymalna ilość znaków to 50")]
    public string Name { get; set; } = null!;

    [Display(Name = "Nazwisko")]
    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    [MaxLength(50, ErrorMessage = "Maksymalna ilość znaków to 50")]
    public string Surname { get; set; } = null!;

    [Required(ErrorMessage = "Email jest wymagany")]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email jest nieprawidłowy")]
    [MaxLength(256, ErrorMessage = "Maksymalna ilość znaków to 256")]
    public string Email { get; set; } = null!;


    [Display(Name = "Telefon")]
    [Required(ErrorMessage = "Telefon jest wymagany")]
    [RegularExpression("^[0-9]{9,9}$", ErrorMessage = "Niepoprawny format telefonu")]
    public string PhoneNumber { get; set; } = null!;

    [Display(Name = "Data Utworzenia")]
    [DataType(DataType.DateTime)]
    public DateTime CreatedDateTime { get; set; }
}