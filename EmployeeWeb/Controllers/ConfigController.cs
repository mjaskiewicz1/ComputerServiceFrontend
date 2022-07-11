using Common.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.Controllers;

[Authorize]
public class ConfigController : Controller
{
    private readonly IConfigService _config;


    public ConfigController(IConfigService config)
    {
        _config = config;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _config.Get());
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromForm] ConfigViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _config.Create(model);

        return View(model);
    }
}