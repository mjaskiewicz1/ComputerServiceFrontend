using System.Net;

namespace Common.Helppers;

public class ClientWebApiReturnValueHelper
{
    public ClientWebApiReturnValueHelper(HttpStatusCode responseStatusCode, string response)
    {
        ResponseStatusCode = responseStatusCode;
        Response = response;
    }

    public string Response { get; set; } = null!;
    public HttpStatusCode ResponseStatusCode { get; set; }
}