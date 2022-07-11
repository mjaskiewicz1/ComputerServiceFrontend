using Azure;
using Common.Dtos;
using Common.Enums;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Common.Services;

public class RequestService : IRequestService
{
    private readonly IActionContextAccessor _actionContextAccessor;
    private readonly IRequestApiRepository _apiRepository;
    private readonly IAzureBlobService _azureBlobService;
    private readonly IEmailSenderService _emailSenderService;
    private readonly IConfigApiRepository _configApiRepository;

    public RequestService(IRequestApiRepository apiRepository, IAzureBlobService azureBlobService,
        IEmailSenderService emailSenderService, IActionContextAccessor actionContextAccessor, IConfigApiRepository configApiRepository)
    {
        _apiRepository = apiRepository;
        _azureBlobService = azureBlobService;
        _emailSenderService = emailSenderService;
        _actionContextAccessor = actionContextAccessor;
        _configApiRepository = configApiRepository;
    }

    public async Task<IEnumerable<RequestViewModel>?> GetAll()
    {
        return await _apiRepository.GetAll();
    }

    public async Task<bool> Create(RequestCreateViewModel model)
    {
        var response = await _apiRepository.Create(model);
        if (response == null) return false;
        if (model.Files == null)
        {
            try
            {
                var config = await _configApiRepository.Get();
                if (config != null && model.RequestStatus == RequestStatus.Nowe)
                {
                    await _emailSenderService.SendUrlNewRequest(model.Email, model.Name,
                        response.Rma ?? throw new InvalidOperationException(),
                        response.Url ?? throw new InvalidOperationException(), config);
                }
                else
                {
                    await _emailSenderService.SendUrlNewRequest(model.Email, model.Name,
                        response.Rma ?? throw new InvalidOperationException(),
                        response.Url ?? throw new InvalidOperationException());

                }

            }
            catch (Exception e)
            {
                _actionContextAccessor.ActionContext.ModelState.AddModelError(string.Empty, "Coś poszło nie tak");

                var rma = new RmaDto
                {
                    Rma = response.Rma
                };
                await _apiRepository.Delete(rma);
                await _azureBlobService.Delete(rma.Rma);
                return false;
            }

            return true;
        }

        try
        {
            if (response.Rma != null)
                await _azureBlobService.UploadBlobsAsync(model.Files, response.Rma);
            else

                return false;
            var config = await _configApiRepository.Get();
            if (config!=null&&model.RequestStatus==RequestStatus.Nowe)
            {
                await _emailSenderService.SendUrlNewRequest(model.Email, model.Name,
                    response.Rma ?? throw new InvalidOperationException(),
                    response.Url ?? throw new InvalidOperationException(),config);
            }
            else
            {
                await _emailSenderService.SendUrlNewRequest(model.Email, model.Name,
                    response.Rma ?? throw new InvalidOperationException(),
                    response.Url ?? throw new InvalidOperationException());

            }

        }
        catch (RequestFailedException e)
        {
            var rma = new RmaDto
            {
                Rma = response.Rma
            };
            await _apiRepository.Delete(rma);
            await _azureBlobService.Delete(rma.Rma);
            return false;
        }
        catch (Exception e)
        {
            var rma = new RmaDto
            {
                Rma = response.Rma
            };
            await _apiRepository.Delete(rma);
            await _azureBlobService.Delete(rma.Rma);
            return false;
        }


        return true;
    }


    public async Task<RequestCreateViewModel?> Get(RmaDto model)
    {
        return await _apiRepository.Get(model);
    }

    public async Task<bool> Update(RequestEditViewModel model)
    {
        try
        {
            if (await _apiRepository.Update(model))
            {
                await _emailSenderService.SendUrlUpdateRequest(model.Email, model.Name, model.Rma, model.Url.ToString(),
                    model.RequestStatus);
                return true;
            }
        }
        catch (Exception e)
        {
            return false;
        }

        return false;
    }

    public async Task<RequestDetailClientViewModel?> Get(RmaUrlDto model)
    {
        return await _apiRepository.Get(model);
    }

    public async Task<IEnumerable<RequestHistoryViewModel>?> GetHistoryAll(RmaDto rma)
    {
        return await _apiRepository.GetHistoryAll(rma);
    }

    public async Task<IEnumerable<RequestViewModel>?> Filter(RequestFilterViewModel model)
    {
        return await _apiRepository.Filter(model);
    }

    public async Task<RequestConversationViewModel?> CreateRequestConversationMessage(
        RequestConversationViewModel model)
    {
        if (!await _apiRepository.CreateRequestConversationMessage(model)) return null;
        model.CreatedDateTime=DateTime.Now.ToLocalTime();
        return model;

    }

    public async Task<RequestEditViewModel?> GetRequestEdit(string rma)
    {
        return await _apiRepository.GetRequestEdit(rma);
    }
}