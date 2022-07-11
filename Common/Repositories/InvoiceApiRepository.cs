using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Common.Dtos;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Newtonsoft.Json;

namespace Common.Repositories;

public class InvoiceApiRepository : BaseRepository, IInvoiceApiRepository
{
    private readonly ITokenAcquisition _tokenAcquisition;

    public InvoiceApiRepository(IClientWebApi clientWebApi, IConfiguration configuration,
        ITokenAcquisition tokenAcquisition) : base(clientWebApi, configuration)
    {
        _tokenAcquisition = tokenAcquisition;
    }

    public async Task<InvoiceCreateViewModel?> GetCreateInvoiceData(string rma)
    {
        var model = new RmaDto
        {
            Rma = rma
        };
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var responseInvoiceDetail = await ClientWebApi.GetResponse(Configuration["AzureFunction:GetCreateInvoiceData"],
            HttpMethod.Get, stringContent);
        if (responseInvoiceDetail.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        return JsonConvert.DeserializeObject<InvoiceCreateViewModel>(responseInvoiceDetail.Response);
    }


    public async Task<InvoiceNumberDto?> Create(InvoiceDto model)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(model)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:CreateInvoice"], HttpMethod.Put,
            stringContent);

        if (response.ResponseStatusCode == HttpStatusCode.Created)
            return JsonConvert.DeserializeObject<InvoiceNumberDto>(response.Response);

        if (response.ResponseStatusCode == HttpStatusCode.Conflict)
            return new InvoiceNumberDto
            {
                Error = response.Response
            };

        return new InvoiceNumberDto
        {
            Error = "Wystapił nieznanyny błąd"
        };
    }

    public async Task<IEnumerable<InvoiceViewModel>?> GetAll()
    {
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:GetAllInvoice"], HttpMethod.Get);
        if (response.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        var model = JsonConvert.DeserializeObject<IEnumerable<InvoiceViewModel>>(response.Response);
        return model;
    }

    public async Task<InvoiceFullDataViewModel?> Get(string invoiceNumber)
    {
        var stringContent = new StringContent(JsonConvert.SerializeObject(new InvoiceNumberDto
            {
                Number = invoiceNumber
            })
            , Encoding.UTF8, "application/json");

        var response =
            await ClientWebApi.GetResponse(Configuration["AzureFunction:GetInvoice"], HttpMethod.Get, stringContent);
        if (response.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        var model = JsonConvert.DeserializeObject<InvoiceFullDataViewModel>(response.Response);
        if (model == null)
            return null;


   
        return model;
    }

    public async Task<IEnumerable<InvoiceItemCreteViewModel>?> GetProducts()
    {
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:GetAllInvoice"], HttpMethod.Get);
        if (response.ResponseStatusCode != HttpStatusCode.OK)
            return null;
        var model = JsonConvert.DeserializeObject<IEnumerable<InvoiceItemCreteViewModel>>(response.Response);
        return model;
    }
}