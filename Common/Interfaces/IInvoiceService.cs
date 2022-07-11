using Common.Helppers;
using Common.ViewModels;

namespace Common.Interfaces;

public interface IInvoiceService
{
    public Task<InvoiceCreateViewModel?> GetCreateInvoiceData(string rma);
    public Task<bool> Create(InvoiceCreateViewModel model);
    public Task<IEnumerable<InvoiceViewModel>?> GetAll();
    public Task<InvoiceFullDataViewModel?> Get(string invoiceNumber);
    public Task<FileHelper?> GetInvoicePdf(string invoiceNumber);
}