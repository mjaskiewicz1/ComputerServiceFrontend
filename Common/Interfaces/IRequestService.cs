using Common.Dtos;
using Common.ViewModels;

namespace Common.Interfaces;

public interface IRequestService
{
    public Task<IEnumerable<RequestViewModel>?> GetAll();
    public Task<bool> Create(RequestCreateViewModel model);
    public Task<RequestCreateViewModel?> Get(RmaDto model);
    public Task<bool> Update(RequestEditViewModel model);
    public Task<RequestDetailClientViewModel?> Get(RmaUrlDto model);
    public Task<IEnumerable<RequestHistoryViewModel>?> GetHistoryAll(RmaDto rma);
    public Task<IEnumerable<RequestViewModel>?> Filter(RequestFilterViewModel model);
    public Task<RequestConversationViewModel?> CreateRequestConversationMessage(RequestConversationViewModel model);
    public Task<RequestEditViewModel?> GetRequestEdit(string rma);
}