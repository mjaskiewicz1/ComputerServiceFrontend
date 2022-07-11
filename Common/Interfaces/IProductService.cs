using Common.ViewModels;

namespace Common.Interfaces;

public interface IProductService
{
    public Task<IEnumerable<ProductViewModel>?> GetAll();
    public Task<ProductViewModel?> Get(string code);
    public Task Delete(string code);
    public Task<bool> Create(ProductViewModel model);
    public Task<bool> Update(ProductViewModel model);
}