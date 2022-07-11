using Common.Enums;
using Common.Helppers;
using Common.ViewModels;

namespace Common.Interfaces;

public interface IEmailSenderService
{
    public Task SendUrlNewRequest(string email, string name, string rma, string url);

    public Task SendUrlUpdateRequest(string email, string name, string rma, string url,
        RequestStatus requestStatus);
    public Task SendUrlNewRequest(string email, string name, string rma, string url,ConfigViewModel config);
    public Task SendInvoice(string email, FileHelper file);
}