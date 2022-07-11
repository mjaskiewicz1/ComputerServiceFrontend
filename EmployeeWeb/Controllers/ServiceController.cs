using Common.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.Controllers;

[Authorize]
public class ServiceController : Controller
{
    private readonly IServiceService _service;

    public ServiceController(IServiceService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _service.GetAll());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ServiceViewModel service)
    {
        {
            if (!ModelState.IsValid) return View(service);
            if (service.Price < 0)
            {
                ModelState.AddModelError("Price", "Cena musi być większa od zera");
                return View(service);
            }

            if (!await _service.Create(service)) return View(service);

            return RedirectToAction(nameof(Index));
        }
    }


    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null) return NotFound();

        var model = await _service.Get(id);
        if (model == null) return NotFound();
        return View(model);
    }

    public async Task<IActionResult> Details(string? id)
    {
        if (id == null) return NotFound();

        var model = await _service.Get(id);
        if (model == null) return NotFound();
        return View(model);
    }

    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null) return NotFound();

        var model = await _service.Get(id);
        if (model == null) return NotFound();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirm(string? id)
    {
        if (id == null) return NotFound();

        await _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ServiceViewModel service)
    {
        if (!ModelState.IsValid) return View(service);
        if (service.Price < 0)
        {
            ModelState.AddModelError("Price", "Cena musi być większa od zera");
            return View(service);
        }



        if (!await _service.Update(service)) return View(service);

        return RedirectToAction(nameof(Index));
    }
}