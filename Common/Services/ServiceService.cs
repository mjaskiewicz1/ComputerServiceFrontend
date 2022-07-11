using Common.Exceptions;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Common.Services;

public class ServiceService : IServiceService
{
    private readonly IActionContextAccessor _actionContextAccessor;
    private readonly IServiceApiRepository _apiRepository;

    public ServiceService(IServiceApiRepository apiRepository, IActionContextAccessor actionContextAccessor)
    {
        _apiRepository = apiRepository;
        _actionContextAccessor = actionContextAccessor;
    }

    public async Task<IEnumerable<ServiceViewModel>?> GetAll()
    {
        return await _apiRepository.GetAll();
    }

    public Task<ServiceViewModel?> Get(string code)
    {
        return _apiRepository.Get(code);
    }

    public async Task Delete(string code)
    {
        await _apiRepository.Delete(code);
    }

    public async Task<bool> Create(ServiceViewModel model)
    {
        try
        {
            await _apiRepository.Create(model);
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

    public async Task<bool> Update(ServiceViewModel model)
    {
        try
        {
            await _apiRepository.Update(model);
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