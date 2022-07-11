using System.Net;
using System.Text;
using Common.Dtos;
using Common.Exceptions;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Common.Repositories;

public class ProductApiRepository : BaseRepository, IProductApiRepository
{
    public ProductApiRepository(IClientWebApi clientWebApi, IConfiguration configuration) : base(clientWebApi,
        configuration)
    {
    }

    public async Task<IEnumerable<ProductViewModel>?> GetAll()
    {
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:ProductGetAll"], HttpMethod.Get);
        if (response.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        return JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(response.Response);
    }

    public async Task<ProductViewModel?> Get(string code)
    {
        var model = new CodeDto
        {
            Code = code
        };
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var responseInvoiceDetail =
            await ClientWebApi.GetResponse(Configuration["AzureFunction:ProductGet"], HttpMethod.Get, stringContent);
        return responseInvoiceDetail.ResponseStatusCode != HttpStatusCode.OK
            ? null
            : JsonConvert.DeserializeObject<ProductViewModel>(responseInvoiceDetail.Response);
    }

    public async Task Delete(string code)
    {
        var model = new CodeDto
        {
            Code = code
        };
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:ProductDelete"], HttpMethod.Delete,
            stringContent);
    }

    public async Task Create(ProductViewModel model)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:ProductCreate"], HttpMethod.Put,
            stringContent);
        if (response.ResponseStatusCode == HttpStatusCode.Conflict)
            throw new UniqueDuplicateException("Name", "Produkt istnieje");
    }

    public async Task Update(ProductViewModel model)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:ProductUpdate"], HttpMethod.Put,
            stringContent);
        if (response.ResponseStatusCode == HttpStatusCode.Conflict)
            throw new UniqueDuplicateException("Name", "Produkt istnieje");
    }
}