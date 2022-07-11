using Common.Helppers;
using Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Common.Repositories;

public class ClientWebApi : IClientWebApi
{
    private readonly HttpClient _client;
    private readonly IConfiguration Configuration;

    public ClientWebApi(IConfiguration configuration, HttpClient client)
    {
        Configuration = configuration;
        _client = client;
        _client = new HttpClient();
        _client.BaseAddress = new Uri(Configuration["AzureFunction:Url"]);
    }


    public async Task<ClientWebApiReturnValueHelper> GetResponse(string action, HttpMethod method)
    {
        var request = new HttpRequestMessage(method,
            action);
        var response = await _client.SendAsync(request);
        var responseS = await response.Content.ReadAsStringAsync();
        var statusCodeResponse = response.StatusCode;
        return new ClientWebApiReturnValueHelper(statusCodeResponse, responseS);
    }

    public async Task<ClientWebApiReturnValueHelper> GetResponse(string action, HttpMethod method,
        StringContent content)
    {
        var request = new HttpRequestMessage(method,
            action);
        request.Content = content;
        var response = await _client.SendAsync(request);
        var responseS = await response.Content.ReadAsStringAsync();

        var statusCodeResponse = response.StatusCode;
        return new ClientWebApiReturnValueHelper(statusCodeResponse, responseS);
    }
}