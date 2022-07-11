using Common.Dtos;

namespace Common.ViewModels;

public class RequestDetailClientViewModel : RequestCreateViewModel
{
    public RequestDetailClientViewModel(RequestDto model) : base(model)
    {
        RequestConversationViewModels = new List<RequestConversationViewModel>();
        foreach (var item in model.RequestConversations)
            RequestConversationViewModels.Add(new RequestConversationViewModel(item.Message, item.EmployeeObjectId,
                item.CreatedDateTime));
    }

    public List<RequestConversationViewModel> RequestConversationViewModels { get; set; }
    public string FullName => $"{Name} {Surname}";
    public RequestConversationViewModel RequestConversationClient { get; set; }
}