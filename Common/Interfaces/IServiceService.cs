using Common.ViewModels;

namespace Common.Interfaces;

public interface IServiceService
{
    public Task<IEnumerable<ServiceViewModel>?> GetAll();
    public Task<ServiceViewModel?> Get(string code);
    public Task Delete(string code);
    public Task<bool> Create(ServiceViewModel model);
    public Task<bool> Update(ServiceViewModel model);
}