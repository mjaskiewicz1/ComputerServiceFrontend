using Common.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.ViewComponents;

public class BlobViewComponent : ViewComponent
{
    private readonly IAzureBlobService _blobService;

    public BlobViewComponent(IAzureBlobService blobService)
    {
        _blobService = blobService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string rma)
    {
        var listblobs = await _blobService.GetAllBlobs(rma);
        if (listblobs == null) return View(new BlobViewModel());

        var model = new BlobViewModel
        {
            Rma = rma,
            NameBlobLists = listblobs
        };
        return View(model);
    }
}