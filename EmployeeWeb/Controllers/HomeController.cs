using System.Diagnostics;
using Common.Interfaces;
using EmployeeWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.Controllers;

public class HomeController : Controller
{
    private readonly IConfigService _configService;

    public HomeController(IConfigService configService)
    {
        _configService = configService;
    }

    public async Task<IActionResult> Index()
    {
        if (await _configService.Exist())
            ViewData["configExist"] = true;
        else
            ViewData["configExist"] = false;
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}