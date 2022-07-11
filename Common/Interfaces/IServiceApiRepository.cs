using Common.ViewModels;

namespace Common.Interfaces;

public interface IServiceApiRepository
{
    public Task<IEnumerable<ServiceViewModel>?> GetAll();
    public Task<ServiceViewModel?> Get(string code);
    public Task Delete(string code);
    public Task Create(ServiceViewModel model);
    public Task Update(ServiceViewModel model);
}