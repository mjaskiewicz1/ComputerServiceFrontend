using Common.ViewModels;

namespace Common.Interfaces;

public interface IProductApiRepository
{
    public Task<IEnumerable<ProductViewModel>?> GetAll();
    public Task<ProductViewModel?> Get(string code);
    public Task Delete(string code);
    public Task Create(ProductViewModel model);
    public Task Update(ProductViewModel model);
}