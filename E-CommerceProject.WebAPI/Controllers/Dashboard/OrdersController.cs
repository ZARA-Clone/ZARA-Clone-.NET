using E_CommerceProject.Business.Brands.Dtos;
using E_CommerceProject.Business.Dashborad.Orders;
using E_CommerceProject.Business.Dashborad.Orders.Dtos;
using E_CommerceProject.Business.Shared;
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
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrdersService ordersService
            , ILogger<OrdersController> logger)
        {
            _ordersService = ordersService;
            _logger = logger;
        }

        
        [HttpGet("getAll")]
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
        [HttpGet]
        public async Task<ActionResult<PageList<OrderReadDto>>> Get(int pageIndex = 0, int pageSize = 10)
        {
            var result = await _ordersService.Get(pageIndex, pageSize);
            _logger.LogInformation($"Get '{result.Items.Count}' products from '{result.TotalCount}'.");
            return result;
        }
    }
}
