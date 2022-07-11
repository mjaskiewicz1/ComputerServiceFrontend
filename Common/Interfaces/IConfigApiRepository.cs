using Common.ViewModels;

namespace Common.Interfaces;

public interface IConfigApiRepository
{
    public Task<ConfigViewModel?> Get();
    public Task<bool> Create(ConfigViewModel model);
    public Task<bool> Exist();
}