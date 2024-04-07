using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Repositories.Brands;
using E_CommerceProject.Infrastructure.Repositories.Dashborad.Data;
using E_CommerceProject.Infrastructure.Repositories.Dashborad.Orders;
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
        private readonly IOrdersRepository _ordersRepository;

        public IBrandRepository BrandRepository => _brandRepository;

        public IProductsRepository ProductsRepository => _productsRepository;

        public IUserDashboardRepository UserDashboardRepository => _userDashboardRepository;

        public IDateRepository DateRepository => _dateRepository;

        public IOrdersRepository OrderRepository => _ordersRepository;

        public UnitOfWorkAsync(ECommerceContext dbContext
            , IBrandRepository brandRepository
            , IProductsRepository productsRepository
            , IUserDashboardRepository userDashboardRepository
            , IDateRepository dateRepository
            ,IOrdersRepository ordersRepository)
        {
            _dbContext = dbContext;
            _brandRepository = brandRepository;
            _productsRepository = productsRepository;
            _userDashboardRepository = userDashboardRepository;
            _dateRepository = dateRepository;
            _ordersRepository = ordersRepository;
        }


        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
