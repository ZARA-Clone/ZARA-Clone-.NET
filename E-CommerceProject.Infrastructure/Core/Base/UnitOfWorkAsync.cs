using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Repositories.Brands;
using E_CommerceProject.Infrastructure.Repositories.Dashborad.Data;
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
        private readonly IDateRepository _dateRepository;

        public IBrandRepository BrandRepository => _brandRepository;

        public IProductsRepository ProductsRepository => _productsRepository;

        public IUserDashboardRepository UserDashboardRepository => _userDashboardRepository;

        public IDateRepository DateRepository => _dateRepository;

        public UnitOfWorkAsync(ECommerceContext dbContext
            , IBrandRepository brandRepository
            , IProductsRepository productsRepository
            , IUserDashboardRepository userDashboardRepository
            , IDateRepository dateRepository)
        {
            _dbContext = dbContext;
            _brandRepository = brandRepository;
            _productsRepository = productsRepository;
            _userDashboardRepository = userDashboardRepository;
            _dateRepository = dateRepository;
        }


        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
