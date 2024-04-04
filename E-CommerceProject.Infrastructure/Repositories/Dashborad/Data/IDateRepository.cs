using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;

namespace E_CommerceProject.Infrastructure.Repositories.Dashborad.Data
{
    public interface IDateRepository : IRepositoryAsync<Order, int>
    {
        Task<List<Order>> GetOrdersWithData();
    }
}
