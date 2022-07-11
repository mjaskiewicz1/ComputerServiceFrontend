using Common.Interfaces;
using Common.ViewModels;

namespace Common.Services;

public class ConfigService : IConfigService
{
    private readonly IConfigApiRepository _apiRepository;

    public ConfigService(IConfigApiRepository apiRepository)
    {
        _apiRepository = apiRepository;
    }

    public async Task<ConfigViewModel?> Get()
    {
        return await _apiRepository.Get();
    }

    public Task<bool> Create(ConfigViewModel model)
    {
        return _apiRepository.Create(model);
    }

    public async Task<bool> Exist()
    {
        return await _apiRepository.Exist();
    }
}