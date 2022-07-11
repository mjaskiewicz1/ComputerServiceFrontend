using System.Net;
using System.Text;
using Common.Dtos;
using Common.Exceptions;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Common.Repositories;

public class ServiceApiRepository : BaseRepository, IServiceApiRepository
{
    public ServiceApiRepository(IClientWebApi clientWebApi, IConfiguration configuration) : base(clientWebApi,
        configuration)
    {
    }

    public async Task<IEnumerable<ServiceViewModel>?> GetAll()
    {
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:ServiceGetAll"], HttpMethod.Get);
        if (response.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        return JsonConvert.DeserializeObject<IEnumerable<ServiceViewModel>>(response.Response);
    }

    public async Task<ServiceViewModel?> Get(string code)
    {
        var model = new CodeDto
        {
            Code = code
        };
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response =
            await ClientWebApi.GetResponse(Configuration["AzureFunction:ServiceGet"], HttpMethod.Get, stringContent);
        return response.ResponseStatusCode != HttpStatusCode.OK
            ? null
            : JsonConvert.DeserializeObject<ServiceViewModel>(response.Response);
    }

    public async Task Delete(string code)
    {
        var model = new CodeDto
        {
            Code = code
        };
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:ServiceDelete"], HttpMethod.Delete,
            stringContent);
    }

    public async Task Create(ServiceViewModel model)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:ServiceCreate"], HttpMethod.Put,
            stringContent);
        if (response.ResponseStatusCode == HttpStatusCode.Conflict)
            throw new UniqueDuplicateException("Name", "Usługa istnieje");
    }

    public async Task Update(ServiceViewModel model)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:ServiceUpdate"], HttpMethod.Put,
            stringContent);
        if (response.ResponseStatusCode == HttpStatusCode.Conflict)
            throw new UniqueDuplicateException("Name", "Usługa istnieje");
    }
}