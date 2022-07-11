using System.Net;
using System.Text;
using Common.Dtos;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Common.Repositories;

public class RequestApiRepository : BaseRepository, IRequestApiRepository
{
    public RequestApiRepository(IClientWebApi clientWebApi, IConfiguration configuration) : base(clientWebApi,
        configuration)
    {
    }

    public async Task<IEnumerable<RequestViewModel>?> GetAll()
    {
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:RequestGetAll"], HttpMethod.Get);
        if (response.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        var model = JsonConvert.DeserializeObject<IEnumerable<RequestViewModel>>(response.Response);
        return model;
    }

    public async Task<RmaUrlDto?> Create(RequestCreateViewModel model)
    {
        var modelTmp = new RequestDto(model);
        var stringContent = new StringContent(JsonConvert.SerializeObject(modelTmp)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:RequestCreate"], HttpMethod.Put,
            stringContent);
        return response.ResponseStatusCode == HttpStatusCode.Created
            ? JsonConvert.DeserializeObject<RmaUrlDto>(response.Response)
            : null;
    }

    public async Task Delete(RmaDto model)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:RequestDelete"], HttpMethod.Delete,
            stringContent);
    }

    public async Task<RequestCreateViewModel?> Get(RmaDto model)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:RequestGetByRma"], HttpMethod.Get,
            stringContent);
        if (response.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        var requestDto = JsonConvert.DeserializeObject<RequestDto>(response.Response);
        if (requestDto == null)
            return null;
        var requestCreateViewModel = new RequestCreateViewModel(requestDto);
        return requestCreateViewModel;
    }

    public async Task<bool> Update(RequestEditViewModel model)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:RequestUpdate"], HttpMethod.Put,
            stringContent);
        return response.ResponseStatusCode == HttpStatusCode.OK;
    }

    public async Task<RequestDetailClientViewModel?> Get(RmaUrlDto model)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:RequestGetByRmaUrl"], HttpMethod.Get,
            stringContent);
        if (response.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        var requestDto = JsonConvert.DeserializeObject<RequestDto>(response.Response);
        if (requestDto == null)
            return null;
        var requestCreateViewModel = new RequestDetailClientViewModel(requestDto);
        return requestCreateViewModel;
    }

    public async Task<IEnumerable<RequestHistoryViewModel>?> GetHistoryAll(RmaDto rma)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(rma)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:RequestGetHistoryAll"], HttpMethod.Get,
            stringContent);
        if (response.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        var model = JsonConvert.DeserializeObject<IEnumerable<RequestHistoryViewModel>>(response.Response);
        return model;
    }

    public async Task<IEnumerable<RequestViewModel>?> Filter(RequestFilterViewModel model)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:RequestFilter"], HttpMethod.Get,
            stringContent);

        if (response.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        var requestView = JsonConvert.DeserializeObject<IEnumerable<RequestViewModel>>(response.Response);
        return requestView;
    }

    public async Task<bool> CreateRequestConversationMessage(
        RequestConversationViewModel model)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:CreateRequestConversationItem"],
            HttpMethod.Put, stringContent);
        return response.ResponseStatusCode == HttpStatusCode.Created;
    }

    public async Task<RequestEditViewModel?> GetRequestEdit(string rma)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(new RmaDto
            {
                Rma = rma
            })
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:GetRequestEditConversationData"],
            HttpMethod.Get, stringContent);
        if (response.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        return JsonConvert.DeserializeObject<RequestEditViewModel>(response.Response);
    }
}