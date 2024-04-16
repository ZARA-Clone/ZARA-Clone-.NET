using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;

namespace E_CommerceProject.Infrastructure.Repositories.Products
{
    public interface IProductsRepository : IRepositoryAsync<Product, int>
    {
        Task<(List<Product> items, int totalItemsCount)> Get(string? name, int? brandId, decimal? minPrice
           , decimal? maxPrice, int pageIndex = 0, int pageSize = 10);

        Task<bool> IsNameExist(Product entity);
    }
}
