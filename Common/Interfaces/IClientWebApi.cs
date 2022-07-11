using Common.Helppers;

namespace Common.Interfaces;

public interface IClientWebApi
{
    public Task<ClientWebApiReturnValueHelper> GetResponse(string action, HttpMethod method);
    public Task<ClientWebApiReturnValueHelper> GetResponse(string action, HttpMethod method, StringContent content);
}