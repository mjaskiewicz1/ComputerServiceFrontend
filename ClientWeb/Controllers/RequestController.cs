using Common.Dtos;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClientWeb.Controllers;

public class RequestController : Controller
{
    private readonly IAzureBlobService _azureBlobService;

    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService, IAzureBlobService azureBlobService)
    {
        _requestService = requestService;
        _azureBlobService = azureBlobService;
    }

    public IActionResult Create()
    {
        return View(new RequestCreateViewModel());
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
        if (response) return RedirectToAction(nameof(SuccessCreated));
        return RedirectToAction(nameof(FailedCreated));
    }


    public async Task<IActionResult> Details(string? rma, string? url)
    {
        if (rma == null || url == null) return NotFound();

        //https://localhost:7156/Request/Details?Url=ccdffa7b-66e1-442e-8557-402eb289bf79&Rma=221271021344
        var model = await _requestService.Get(new RmaUrlDto
        {
            Rma = rma,
            Url = url
        });

        if (model != null) return View(model);

        return NotFound();
    }

    public IActionResult SuccessCreated()
    {
        return View();
    }

    public IActionResult FailedCreated()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DownloadAttachment(string rma, string name)
    {
        var model = await _azureBlobService.DownloadAsync(rma, name);
        return File(model.FileStream, model.ContentType, model.Name);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessageChat([FromForm] RequestConversationViewModel requestConversation)
    {
        var model = await _requestService.CreateRequestConversationMessage(requestConversation);
        if (model != null) return Json(model);

        return BadRequest("eeqqwqw");
    }
}