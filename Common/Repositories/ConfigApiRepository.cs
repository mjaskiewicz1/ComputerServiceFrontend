using System.Net;
using System.Text;
using Common.Dtos;
using Common.Interfaces;
using Common.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Common.Repositories;

public class ConfigApiRepository : BaseRepository, IConfigApiRepository
{
    public ConfigApiRepository(IClientWebApi clientWebApi, IConfiguration configuration) : base(clientWebApi,
        configuration)
    {
    }

    public async Task<ConfigViewModel?> Get()
    {
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:GetConfig"], HttpMethod.Get);
        if (response.ResponseStatusCode != HttpStatusCode.OK) return null;
        var model = JsonConvert.DeserializeObject<ConfigViewModel>(response.Response);
        return model;
    }

    public async Task<bool> Create(ConfigViewModel model)
    {
        var configDto = new ConfigDto(model.Id, model.Name, model.Nip, model.PhoneNumber, model.AddressViewModel.City,
            model.AddressViewModel.Postcode, model.AddressViewModel.Street, model.BankAccountNumber, model.PostalTown,
            model.BankName, model.Email, true);
        var stringContent = new StringContent(JsonConvert.SerializeObject(configDto)
            , Encoding.UTF8, "application/json");
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:CreateConfig"], HttpMethod.Put,
            stringContent);
        return response.ResponseStatusCode == HttpStatusCode.Created;
    }

    public async Task<bool> Exist()
    {
        var response = await ClientWebApi.GetResponse(Configuration["AzureFunction:ExistConfig"], HttpMethod.Get);
        if (response.ResponseStatusCode == HttpStatusCode.OK)
            return true;
        return false;
    }
}