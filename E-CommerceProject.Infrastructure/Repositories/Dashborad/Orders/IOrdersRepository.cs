using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories.Dashborad.Orders
{
    public interface IOrdersRepository : IRepositoryAsync<Order, int>
    {
        Task<(List<Order> items, int totalItemsCount)> Get(int pageIndex, int pageSize);
        Task<List<Order>> GetOrdersWithData();
        Task<Order> GetOrderWithProducts(int OrderId);
        Task<int> GetLastUserOrder(string userId);
    }
    public class OrderRepository : RepositoryAsync<Order, int>, IOrdersRepository
    {
        private readonly ECommerceContext _dbContext;

        public OrderRepository(ECommerceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(List<Order> items, int totalItemsCount)> Get(int pageIndex, int pageSize)
        {
            var orders = _dbContext.Set<Order>()
                .Include(c => c.User)
                .Include(c => c.OrdersDetails)
                    .ThenInclude(c => c.Product)
                        .ThenInclude(c => c.ProductImages)
                .Include(c => c.OrdersDetails)
                    .ThenInclude(c => c.Product)
                        .ThenInclude(c => c.ProductSizes)
                .AsQueryable();

            var totalItems = await orders.CountAsync();
            var items = await orders.OrderBy(c => c.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }

        public async Task<int> GetLastUserOrder(string userId)
        {
              var result =await  _dbContext.Set<Order>()
                            .Where(o => o.UserId == userId)
                            .OrderByDescending(o => o.OrderDate)
                            .FirstOrDefaultAsync(c => c.UserId == userId);
            return result ==null ? result.Id : 0;
        }

        public async Task<List<Order>> GetOrdersWithData()
        {
            var result = await _dbContext.Set<Order>()
                .Include(c => c.User)
                .Include(c => c.OrdersDetails)
                    .ThenInclude(c => c.Product)
                        .ThenInclude(c => c.Brand)
                .Include(c => c.OrdersDetails)
                    .ThenInclude(c => c.Product)
                        .ThenInclude(c => c.ProductImages)
                .Include(c => c.OrdersDetails)
                    .ThenInclude(c => c.Product)
                        .ThenInclude(c => c.ProductSizes)
                .ToListAsync();

            return result;
        }

        public async Task<Order> GetOrderWithProducts(int OrderId)
        {
               var result = await _dbContext.Set<Order>()
                    .Include(o => o.User)
                    .Include(o => o.OrdersDetails)
                        .ThenInclude(op => op.Product)
                            .ThenInclude(p => p.Brand)
                    .Include(o => o.OrdersDetails)
                        .ThenInclude(op => op.Product)
                            .ThenInclude(p => p.ProductImages)
                    .Include(o => o.OrdersDetails)
                        .ThenInclude(op => op.Product)
                            .ThenInclude(p => p.ProductSizes)
                    .FirstOrDefaultAsync(o => o.Id == OrderId);
            return result;
        }
    }
}
