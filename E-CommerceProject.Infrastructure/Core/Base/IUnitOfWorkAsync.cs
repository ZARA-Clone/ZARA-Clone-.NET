using E_CommerceProject.Infrastructure.Repositories.Brands;
using E_CommerceProject.Infrastructure.Repositories.Products;

namespace E_CommerceProject.Infrastructure.Core.Base
{
    public interface IUnitOfWorkAsync
    {
        public IBrandRepository BrandRepository
        {
            get;
        }
        public IProductsRepository ProductsRepository
        {
            get;
        }
        Task SaveAsync();
    }
}
