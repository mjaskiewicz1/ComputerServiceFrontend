using System.ComponentModel.DataAnnotations;

namespace Common.ViewModels;

public class RequestCreateMessageEmployee : RequestConversationViewModel
{
    [Required] public new string EmployeeObjectId { get; set; } = null!;
}