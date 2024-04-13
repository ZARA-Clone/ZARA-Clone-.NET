using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;
using E_CommerceProject.Models.Models;

namespace E_CommerceProject.Infrastructure.Repositories.UsersDashboard
{
    public interface IUserDashboardRepository : IRepositoryAsync<ApplicationUser, string>
    {
        Task<(List<ApplicationUser> items, int totalItemsCount)> Get(int pageIndex, int pageSize);
    }
}
