using Common.Dtos;
using Common.Enums;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.Controllers;

[Authorize]
public class RequestController : Controller
{
    private readonly IAzureBlobService _azureBlobService;

    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService,
        IAzureBlobService azureBlobService)
    {
        _requestService = requestService;
        _azureBlobService = azureBlobService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _requestService.GetAll();
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new RequestCreateViewModel(RequestStatus.Przyjęte));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] RequestCreateViewModel requestCreateViewModel)
    {
        if (requestCreateViewModel.ClientType != "Firma")
        {
            ModelState.Remove("RequestInvoiceDetailCompanyViewModel.Nip");
            ModelState.Remove("RequestInvoiceDetailCompanyViewModel.NameCompany");
            ModelState.Remove("RequestInvoiceDetailCompanyViewModel.AddressViewModel.City");
            ModelState.Remove("RequestInvoiceDetailCompanyViewModel.AddressViewModel.Street");
            ModelState.Remove("RequestInvoiceDetailCompanyViewModel.AddressViewModel.Postcode");
        }
        else
        {
            ModelState.Remove("RequestInvoiceDetailIndividualClientViewModel.Name");
            ModelState.Remove("RequestInvoiceDetailIndividualClientViewModel.Surname");
            ModelState.Remove("RequestInvoiceDetailIndividualClientViewModel.AddressViewModel.City");
            ModelState.Remove("RequestInvoiceDetailIndividualClientViewModel.AddressViewModel.Street");
            ModelState.Remove("RequestInvoiceDetailIndividualClientViewModel.AddressViewModel.Postcode");
        }

        if (requestCreateViewModel.ShipmentType != "Wysyłka")
        {
            ModelState.Remove("RequestShipmentDetailViewModel.City");
            ModelState.Remove("RequestShipmentDetailViewModel.Street");
            ModelState.Remove("RequestShipmentDetailViewModel.Postcode");
        }

        if (!ModelState.IsValid) return View(requestCreateViewModel);


        var response = await _requestService.Create(requestCreateViewModel);
        if (response) return RedirectToAction(nameof(Index));

        return View(requestCreateViewModel);
    }

    public async Task<IActionResult> Details(string? id)
    {
        if (id == null) return NotFound();


        var model = await _requestService.Get(new RmaDto
        {
            Rma = id
        });

        if (model != null) return View(model);

        return NotFound();
    }

    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null) return NotFound();


        var model = await _requestService.GetRequestEdit(id);

        if (model != null) return View(model);

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] RequestEditViewModel model)
    {
        if (await _requestService.Update(model)) return RedirectToAction(nameof(Index));

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> DownloadAttachment(string rma, string name)
    {
        var model = await _azureBlobService.DownloadAsync(rma, name);
        return File(model.FileStream, model.ContentType, model.Name);
    }

    [HttpPost]
    public async Task<IActionResult> Filter([FromForm] RequestFilterViewModel model)
    {
        return View(nameof(Index), await _requestService.Filter(model));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessageChat([FromForm] RequestCreateMessageEmployee createMessageEmployee)

    {
        var model = await _requestService.CreateRequestConversationMessage(createMessageEmployee);
        if (model != null) return Json(model);

        return BadRequest();
    }
}