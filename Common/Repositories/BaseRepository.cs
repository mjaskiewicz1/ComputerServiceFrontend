using Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Common.Repositories;

public abstract class BaseRepository
{
    protected readonly IClientWebApi ClientWebApi;
    protected readonly IConfiguration Configuration;

    protected BaseRepository(IClientWebApi clientWebApi, IConfiguration configuration)
    {
        ClientWebApi = clientWebApi;
        Configuration = configuration;
    }
}