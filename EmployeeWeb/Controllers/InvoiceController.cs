using Common.Enums;
using Common.Exstensions;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.Controllers;

/// <summary>
///     Walidacja requestu
///     Zwracanie widoku
/// </summary>
[Authorize]
public class InvoiceController : Controller
{
    private readonly IConfigService _configService;
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService, IConfigService configService)
    {
        _invoiceService = invoiceService;
        _configService = configService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _invoiceService.GetAll();
        if (model == null) return View();
        return View(model);
    }

    public async Task<IActionResult> ExistConfig(string? id)
    {
        if (await _configService.Exist())
            return Json(new { redirectToUrl = Url.Action("Create", "Invoice", new { id }) });

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> Create(string? id)
    {
        if (id == null) return NotFound();
        
        var model = await _invoiceService.GetCreateInvoiceData(id);

        if (model == null) return NotFound();
        if (model.RequestStatus == RequestStatus.Naprawione || model.RequestStatus == RequestStatus.Wysłane)
        {
            return View(model);
        }

        return RedirectToAction("Index", "Request");

    }

    [HttpPut]
    public async Task<IActionResult> Create([FromForm] InvoiceCreateViewModel model)
    {
       
        ModelState.Clear();
        if (model.Total == 0m)
        {
            ModelState.AddModelError(string.Empty, "Lista produktów nie może być pusta");
            var errors = ModelState.Errors();
            return BadRequest(new { Errors = errors });
        }

        if (model.InvoicePayment < model.InvoiceDate)
        {
            ModelState.AddModelError("InvoicePayment", "Nieprawidłowa data płatności");
            var errors = ModelState.Errors();
            return BadRequest(new { Errors = errors });
        }

        var result = await _invoiceService.Create(model);
        if (result) return Json(new { redirectToUrl = Url.Action("Index", "Invoice") });

        return BadRequest(new { Errors = ModelState.Errors() });
    }
    //[HttpPost]
    //public async Task<IActionResult> CreateInvoice(Dictionary<string, int> model)
    //{
    //    bool result = await _service.CreateInvoice(model);

    //    return Json(new { redirectToUrl = Url.Action("Index", "Invoice") });
    //}
    public async Task<IActionResult> Download(string? id)
    {
        //https://www.kambu.pl/blog/how-to-generate-pdf-from-html-in-net-core-applications/
        if (id == null) return NotFound();

        id = id.Replace("-", "/");
        var model = await _invoiceService.GetInvoicePdf(id);
        if (model == null) return NotFound();

        //var model2 = await _service.GetFullDataInvoice(id);
        //return PartialView("Templates/InvoiceTemplate", model2);
        return File(model.FileStream, model.ContentType, model.Name);
    }
}