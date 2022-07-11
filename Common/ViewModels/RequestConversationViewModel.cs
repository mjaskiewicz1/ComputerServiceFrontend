using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Common.ViewModels;

public class RequestConversationViewModel
{

    [JsonConstructor]
    public RequestConversationViewModel(string rma, string message, string? employeeObjectId, DateTime createdDateTime)
    {
        Rma = rma;
        Message = message;
        EmployeeObjectId = employeeObjectId;
        CreatedDateTime = createdDateTime.ToLocalTime();
    }

    public RequestConversationViewModel(string message, string? employeeObjectId, DateTime createdDateTime)
    {
        Message = message;
        EmployeeObjectId = employeeObjectId;
        CreatedDateTime = createdDateTime;
    }

    public RequestConversationViewModel()
    {
    }

    public string? Rma { get; set; }

    [Display(Name = "Wiadomość")]
    [Required(ErrorMessage = "Widomość nie może być pusta")]
    [MaxLength(250, ErrorMessage = "Wiadomość nie może być dłuższa niż 250 znaków")]
    public string Message { get; set; } = null!;

    public string? EmployeeObjectId { get; set; }
    public DateTime CreatedDateTime { get; set; }
}