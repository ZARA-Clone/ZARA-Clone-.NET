using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;

namespace E_CommerceProject.Infrastructure.Repositories.Brands
{
    public interface IBrandRepository : IRepositoryAsync<Brand, int>
    {
        Task<bool> IsNameExist(Brand entity);
    }
}
