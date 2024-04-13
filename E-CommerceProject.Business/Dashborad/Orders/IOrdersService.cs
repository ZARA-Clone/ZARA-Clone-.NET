using E_CommerceProject.Business.Dashborad.Orders.Dtos;
using E_CommerceProject.Business.Shared;

namespace E_CommerceProject.Business.Dashborad.Orders
{
    public interface IOrdersService
    {
        Task<PageList<OrderReadDto>> Get(int pageIndex, int pageSize);
        Task<List<OrderReadDto>> GetAllOrders();
        Task<OrderDetailsDto> GetOrderDetails(int OrderId);
        Task<ServiceResponse> Delete(int id);
    }
}
