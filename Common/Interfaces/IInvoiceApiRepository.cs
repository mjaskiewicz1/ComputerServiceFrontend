using Common.Dtos;
using Common.ViewModels;

namespace Common.Interfaces;

public interface IInvoiceApiRepository
{
    public Task<InvoiceCreateViewModel?> GetCreateInvoiceData(string rma);
    public Task<InvoiceNumberDto?> Create(InvoiceDto model);
    public Task<IEnumerable<InvoiceViewModel>?> GetAll();
    public Task<InvoiceFullDataViewModel?> Get(string invoiceNumber);
}