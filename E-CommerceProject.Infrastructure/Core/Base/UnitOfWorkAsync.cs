using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Repositories.Brands;
using E_CommerceProject.Infrastructure.Repositories.Products;
using E_CommerceProject.Infrastructure.Repositories.UsersDashboard;

namespace E_CommerceProject.Infrastructure.Core.Base
{
    public class UnitOfWorkAsync : IUnitOfWorkAsync
    {
        private readonly ECommerceContext _dbContext;
        private readonly IBrandRepository _brandRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IUserDashboardRepository _userDashboardRepository;

        public IBrandRepository BrandRepository => _brandRepository;

        public IProductsRepository ProductsRepository => _productsRepository;

        public IUserDashboardRepository UserDashboardRepository => _userDashboardRepository;

        public UnitOfWorkAsync(ECommerceContext dbContext
            , IBrandRepository brandRepository
            , IProductsRepository productsRepository
            , IUserDashboardRepository userDashboardRepository)
        {
            _dbContext = dbContext;
            _brandRepository = brandRepository;
            _productsRepository = productsRepository;
            _userDashboardRepository = userDashboardRepository;
        }


        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
