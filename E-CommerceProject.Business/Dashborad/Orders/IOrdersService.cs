using E_CommerceProject.Business.Brands.Dtos;
using E_CommerceProject.Business.Dashborad.Orders.Dtos;
using E_CommerceProject.Business.Shared;

namespace E_CommerceProject.Business.Dashborad.Orders
{
    public interface IOrdersService
    {
        Task<List<OrderReadDto>> GetAllOrders();
        Task<OrderDetailsDto> GetOrderDetails(int OrderId);
        Task<ServiceResponse> Delete(int id);
    }
}
