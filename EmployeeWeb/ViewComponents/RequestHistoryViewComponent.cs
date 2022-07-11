using Common.Dtos;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.ViewComponents;

public class RequestHistoryViewComponent : ViewComponent
{
    private readonly IRequestService _requestService;

    public RequestHistoryViewComponent(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public async Task<IViewComponentResult> InvokeAsync(string? rma)
    {
        if (rma == null) return await Task.FromResult<IViewComponentResult>(Content(string.Empty));
        var model = await _requestService.GetHistoryAll(new RmaDto
        {
            Rma = rma
        });
        if (model == null) return await Task.FromResult<IViewComponentResult>(Content(string.Empty));

        return View(model);
    }
}