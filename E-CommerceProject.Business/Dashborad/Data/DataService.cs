using E_CommerceProject.Business.Dashborad.Data.Dtos;
using E_CommerceProject.Business.Dashborad.Orders.Dtos;
using E_CommerceProject.Infrastructure.Core.Base;

namespace E_CommerceProject.Business.Dashborad.Data
{
    public class DataService : IDataService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;

        public DataService(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DataReadDto> GetDataDashboard()
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersWithData();
            var sells = orders.Select(o => new OrderReadDto
            {
                TotalPrice = o.OrdersDetails
                .Sum(p => Math.Round((p.Product.Price - (p.Product.Price * (p.Product.Discount / 100))) * p.Quantity, 0))
            });

            var usersCount = _unitOfWork.UserDashboardRepository.GetAllAsync().Result.Count();
            var productsCount = _unitOfWork.ProductsRepository.GetAllAsync().Result.Count();
            var ordersCount = _unitOfWork.OrderRepository.GetAllAsync().Result.Count();

            var result = new DataReadDto
            {
                TotalUsers = usersCount,
                TotalProducts = productsCount,
                TotalSells = sells.Sum(c => c.TotalPrice),
            };
            return result;
        }
    }
}
