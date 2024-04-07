using E_CommerceProject.Business.Dashborad.Orders;
using E_CommerceProject.Business.Dashborad.Orders.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers.Dashboard
{
    [Authorize(Roles = "Admin")]
    [Route("dashboard/api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly ILogger _logger;

        public OrdersController(IOrdersService ordersService
            , ILogger logger)
        {
            _ordersService = ordersService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderReadDto>>> GetAllOrders()
        {
            _logger.LogInformation($"Get all orders");
            var result = await _ordersService.GetAllOrders();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<OrderDetailsDto>> GetOrderDetails(int id)
        {
            _logger.LogInformation($"Get order details with id: {id}");
            var orderDetails = await _ordersService.GetOrderDetails(id);
            if (orderDetails == null)
            {
                _logger.LogWarning($"There is no order with id: {id}");
                return NotFound();
            }

            return Ok(orderDetails);
        }
    }
}
