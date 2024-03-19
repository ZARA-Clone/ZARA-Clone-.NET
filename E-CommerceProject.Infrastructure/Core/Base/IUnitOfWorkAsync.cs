using E_CommerceProject.Infrastructure.Repositories.Brands;

namespace E_CommerceProject.Infrastructure.Core.Base
{
    public interface IUnitOfWorkAsync
    {
        public IBrandRepository BrandRepository { get; }
        Task SaveAsync();
    }
}
