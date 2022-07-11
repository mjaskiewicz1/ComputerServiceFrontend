using Common.Exceptions;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWeb.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    // GET
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _productService.GetAll());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel product)
    {
        if (!ModelState.IsValid) return View(product);
        if (product.Price < 0)
        {
            ModelState.AddModelError("Price","Cena musi być większa od zera");
            return View(product);
        }
        try
        {
            await _productService.Create(product);
            return RedirectToAction(nameof(Index));
        }
        catch (UniqueDuplicateException e)
        {
            ModelState.AddModelError(e.Key, e.Description);
            return View(product);
        }
    }

    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null) return NotFound();

        var model = await _productService.Get(id);
        if (model == null) return NotFound();
        return View(model);
    }

    public async Task<IActionResult> Details(string? id)
    {
        if (id == null) return NotFound();

        var model = await _productService.Get(id);
        if (model == null) return NotFound();
        return View(model);
    }

    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null) return NotFound();

        var model = await _productService.Get(id);
        if (model == null) return NotFound();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirm(string? id)
    {
        if (id == null) return NotFound();

        await _productService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductViewModel product)
    {
        if (!ModelState.IsValid) return View(product);
        if (product.Price < 0)
        {
            ModelState.AddModelError("Price", "Cena musi być większa od zera");
            return View(product);
        }


        if (!await _productService.Update(product)) return View(product);
        return RedirectToAction(nameof(Index));
    }
}