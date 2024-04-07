using E_CommerceProject.Infrastructure.Repositories.Brands;
using E_CommerceProject.Infrastructure.Repositories.Dashborad.Data;
using E_CommerceProject.Infrastructure.Repositories.Dashborad.Orders;
using E_CommerceProject.Infrastructure.Repositories.Products;
using E_CommerceProject.Infrastructure.Repositories.UsersDashboard;

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
        public IUserDashboardRepository UserDashboardRepository
        {
            get;
        }
        public IDateRepository DateRepository
        {
            get;
        }
        public IOrdersRepository OrderRepository
        {
            get;
        }
        Task SaveAsync();
    }
}
