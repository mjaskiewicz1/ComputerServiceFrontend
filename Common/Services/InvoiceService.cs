using Common.Dtos;
using Common.Helppers;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace Common.Services;


public class InvoiceService : IInvoiceService
{
    private readonly IActionContextAccessor _actionContextAccessor;
    private readonly IEmailSenderService _emailService;
    private readonly IInvoiceApiRepository _repository;
    private readonly ITemplateService _templateService;

    public InvoiceService(IInvoiceApiRepository repository, ITemplateService templateService,
        IEmailSenderService emailService, IActionContextAccessor actionContextAccessor)
    {
        _repository = repository;
        _templateService = templateService;
        _emailService = emailService;
        _actionContextAccessor = actionContextAccessor;
    }


    public async Task<InvoiceCreateViewModel?> GetCreateInvoiceData(string rma)
    {
        return await _repository.GetCreateInvoiceData(rma);
    }

    public async Task<bool> Create(InvoiceCreateViewModel model)
    {
        var invoiceItemDtos = new List<InvoiceItemDto>();
        var opt = true;
        foreach (var item in model.InvoiceItems)
        {
            if (item.Amount == null || item.Amount <= 0)
            {
                opt = false;
                break;
            }

            invoiceItemDtos.Add(new InvoiceItemDto(item.Amount, item.BasicUnit, item.Code, item.Name, item.Price,
                item.Tax));
        }

        if (!opt) return false;
        var Total = 0m;
        foreach (var item in invoiceItemDtos)
        {
            var netvalue = item.Price * item.Amount.Value;
            Total += netvalue * (1 + item.Tax);
        }

        var invoice = new InvoiceDto(model.AddressViewModel.City, model.EmployeeObjectId, model.InvoiceDate,
            model.InvoicePayment, invoiceItemDtos,
            model.Name, model.NameCompany, model.Nip, model.PaymentsMethod,
            model.PaymentStatus, model.AddressViewModel.Postcode, model.Rma, model.SaleDate,
            model.AddressViewModel.Street,
            model.Surname, Total);


        var invoiceNumber = await _repository.Create(invoice);
        if (invoiceNumber != null && !string.IsNullOrEmpty(invoiceNumber.Number))
            try
            {
                var invoiceFullData = await _repository.Get(invoiceNumber.Number);
                if (invoiceFullData != null)
                {
                    var file = await GenerateInvoicePdfFile(invoiceFullData);
                    if (file != null) await _emailService.SendInvoice(invoiceFullData.ClientEmail, file);


                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }

        _actionContextAccessor.ActionContext?.ModelState.AddModelError(string.Empty, invoiceNumber.Error);
        return false;
    }

    public async Task<IEnumerable<InvoiceViewModel>?> GetAll()
    {
        return await _repository.GetAll();
    }

    public Task<InvoiceFullDataViewModel?> Get(string invoiceNumber)
    {
        return _repository.Get(invoiceNumber);
    }

    public async Task<FileHelper?> GetInvoicePdf(string invoiceNumber)
    {
        var model = await Get(invoiceNumber);
        if (model == null) return null;

        return await GenerateInvoicePdfFile(model);
    }

    private async Task<FileHelper?> GenerateInvoicePdfFile(InvoiceFullDataViewModel model)
    {
        var html = await _templateService.RenderAsync("Templates/InvoiceTemplate", model);
        //BrowserWSEndpoint from  https://docs.browserless.io/
        await using var browser = await Puppeteer.ConnectAsync(new ConnectOptions
        {
            BrowserWSEndpoint = ""
        });

        await using var page = await browser.NewPageAsync();
        await page.EmulateMediaTypeAsync(MediaType.Screen);
        await page.SetContentAsync(html);
        var marginOptions = new MarginOptions();
        marginOptions.Bottom = "2.5cm";
        marginOptions.Left = "2.5cm";
        marginOptions.Right = "2.5cm";
        marginOptions.Top = "2.5cm";
        var pdfContent = await page.PdfStreamAsync(new PdfOptions
        {
            Format = PaperFormat.A4,
            PrintBackground = true,
            MarginOptions = marginOptions
        });

        return new FileHelper("application/pdf", pdfContent, $"Faktura-{model.Number}.pdf");
    }

}