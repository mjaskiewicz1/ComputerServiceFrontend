using Common.Exceptions;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Common.Services;

public class ProductService : IProductService
{
    private readonly IActionContextAccessor _actionContextAccessor;
    private readonly IProductApiRepository _productApiRepository;

    public ProductService(IProductApiRepository productApiRepository, IActionContextAccessor actionContextAccessor)
    {
        _productApiRepository = productApiRepository;
        _actionContextAccessor = actionContextAccessor;
    }

    public async Task<IEnumerable<ProductViewModel>?> GetAll()
    {
        return await _productApiRepository.GetAll();
    }

    public async Task<ProductViewModel?> Get(string code)
    {
        return await _productApiRepository.Get(code);
    }

    public async Task Delete(string code)
    {
        await _productApiRepository.Delete(code);
    }

    public async Task<bool> Create(ProductViewModel model)
    {
        try
        {
            await _productApiRepository.Create(model);
            return true;
        }
        catch (UniqueDuplicateException e)
        {
            _actionContextAccessor.ActionContext?.ModelState.AddModelError(e.Key, e.Description);
            return false;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Update(ProductViewModel model)
    {
        try
        {
            await _productApiRepository.Update(model);
            return true;
        }
        catch (UniqueDuplicateException e)
        {
            _actionContextAccessor.ActionContext?.ModelState.AddModelError(e.Key, e.Description);
            return false;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}