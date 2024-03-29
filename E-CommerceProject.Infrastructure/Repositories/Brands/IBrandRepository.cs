using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;

namespace E_CommerceProject.Infrastructure.Repositories.Brands
{
    public interface IBrandRepository : IRepositoryAsync<Brand, int>
    {
        Task<(List<Brand> items, int totalItemsCount)> Get(string? name
            , int? categoryId,  int pageIndex = 0, int pageSize = 10);
        Task<bool> IsNameExist(Brand entity);
    }
}
