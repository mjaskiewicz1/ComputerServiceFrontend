using System.ComponentModel.DataAnnotations;
using Common.Dtos;
using Common.Enums;

namespace Common.ViewModels;

public class RequestEditViewModel
{
    public RequestEditViewModel()
    {
    }

    public RequestEditViewModel(RequestDto requestDto)
    {
        Rma = requestDto.Rma!;
        RequestStatus = requestDto.RequestStatus;
        Url = requestDto.Url;
        Email = requestDto.Email;
        Name = requestDto.Name;
    }

    public string Rma { get; set; }
    public Guid Url { get; set; }

    [Display(Name = "Status")] public RequestStatus RequestStatus { get; set; }

    public string? EmployeeObjectId { get; set; }
    public string Email { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    
    public decimal? Estimate { get; set; }

    public RequestCreateMessageEmployee? NewMessageEmployee { get; set; }

    public List<RequestConversationViewModel> RequestConversations { get; set; } = new();
}