using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.Infrastructure.Repositories.Dashborad.Data
{
    public class DateRepository : RepositoryAsync<Order, int>, IDateRepository
    {
        private readonly ECommerceContext _dbContext;

        public DateRepository(ECommerceContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;    
        }

        public async Task<List<Order>> GetOrdersWithData()
        {
            var result = await _dbContext.Set<Order>()
                .Include(c => c.User)
                .Include(c => c.OrdersDetails)
                    .ThenInclude(c => c.Product)
                        .ThenInclude(c => c.ProductImages)
                .Include(c => c.OrdersDetails)
                    .ThenInclude(c => c.Product)
                        .ThenInclude(c => c.ProductSizes)
                .ToListAsync();

            return result;
        }
    }
}
