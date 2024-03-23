using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Repositories.Brands;
using E_CommerceProject.Infrastructure.Repositories.Products;

namespace E_CommerceProject.Infrastructure.Core.Base
{
    public class UnitOfWorkAsync : IUnitOfWorkAsync
    {
        private readonly ECommerceContext _dbContext;
        private readonly IBrandRepository _brandRepository;
        private readonly IProductsRepository _productsRepository;

        public IBrandRepository BrandRepository => _brandRepository;

        public IProductsRepository ProductsRepository => _productsRepository;

        public UnitOfWorkAsync(ECommerceContext dbContext
            , IBrandRepository brandRepository
            , IProductsRepository productsRepository)
        {
            _dbContext = dbContext;
            _brandRepository = brandRepository;
            _productsRepository = productsRepository;
        }


        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
