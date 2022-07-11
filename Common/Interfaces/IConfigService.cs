using Common.ViewModels;

namespace Common.Interfaces;

public interface IConfigService
{
    public Task<ConfigViewModel?> Get();
    public Task<bool> Create(ConfigViewModel model);
    public Task<bool> Exist();
}