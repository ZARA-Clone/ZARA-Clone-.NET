using E_CommerceProject.Business.Dashborad.Orders.Dtos;
using E_CommerceProject.Business.Shared;
using E_CommerceProject.Infrastructure.Core.Base;

namespace E_CommerceProject.Business.Dashborad.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;

        public OrdersService(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PageList<OrderReadDto>> Get(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.OrderRepository.Get(pageIndex, pageSize);
            var orderDtos = result.items
                .Select(o => new OrderReadDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    UserId = o.User.Id,
                    UserName = (o.User.UserName),
                    ProductCount = o.OrdersDetails.Count(),
                    TotalPrice = o.OrdersDetails
                    .Sum(op => Math.Round((op.Product.Price - (op.Product.Price * (op.Product.Discount / 100))) * op.Quantity, 0)),
                }).ToList();

            return new PageList<OrderReadDto>(orderDtos, pageIndex, pageSize, result.totalItemsCount);
        }

        public async Task<List<OrderReadDto>> GetAllOrders()
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersWithData();
            if (orders is null)
            {
                return null;
            }
            var orderDtos = orders
                .Select(o => new OrderReadDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    UserId = o.User.Id,
                    UserName = (o.User.UserName),
                    ProductCount = o.OrdersDetails.Count(),
                    TotalPrice = o.OrdersDetails
                    .Sum(op => Math.Round((op.Product.Price - (op.Product.Price * (op.Product.Discount / 100))) * op.Quantity, 0)),
                });

            return orderDtos.ToList();
        }

        public async Task<OrderDetailsDto> GetOrderDetails(int OrderId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithProducts(OrderId);

            if (order is null)
            {
                return null;
            }

            var orderDetailsDto = new OrderDetailsDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                UserId = order.User.Id,
                UserName = (order.User.UserName),
                ProductCount = order.OrdersDetails.Count(),
                TotalPrice = order.OrdersDetails.Sum(op => Math.Round((op.Product.Price - (op.Product.Price * (op.Product.Discount / 100))) * op.Quantity, 0)),
                OrderProducts = order.OrdersDetails.Select(op => new OrderProductsDto
                {
                    Quantity = op.Quantity,
                    Name = op.Product.Name,
                    Price = op.Product.Price,
                    ImageUrl = op.Product.ProductImages.FirstOrDefault()?.Url ?? "",
                    Discount = op.Product.Discount,
                    ProductId = op.ProductId
                }).ToList(),
                Phone = order.User.PhoneNumber,
            };

            return orderDetailsDto;
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
                throw new ArgumentNullException("id", $"There is no brand with id: {id}");

            await _unitOfWork.OrderRepository.DeleteAsync(order);
            await _unitOfWork.SaveAsync();
            return ServiceResponse.Success();
        }

        
    }
}
