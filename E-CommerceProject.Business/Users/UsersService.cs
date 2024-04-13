using E_CommerceProject.Business.Shared;
using E_CommerceProject.Business.Users.Dtos;
using E_CommerceProject.Business.Users.Interfaces;
using E_CommerceProject.Infrastructure.Core.Base;

namespace E_CommerceProject.Business.Users
{
    public class UsersService : IUserService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;

        public UsersService(IUnitOfWorkAsync unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PageList<UserDto>> Get(int pageIndex = 0, int pageSize = 10)
        {
            var users = await _unitOfWork.UserDashboardRepository.Get(pageIndex, pageSize);
            if (users.items.Any())
            {
                var result = users.items.Select(user => new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Country = user.Country,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Orders = user.Orders.Select(o => new UserDashboardOrderDto
                    {
                        Id = o.Id,
                        OrderDate = o.OrderDate,
                        OriginalPrice = o.OrdersDetails
                        .Sum(op => op.Price * op.Quantity),
                        NetPrice = o.OrdersDetails.Sum(op => Math.Round((op.Price - (op.Price * (op.Product.Discount / 100))) * op.Quantity, 0)),
                    })
                }).ToList();

                return new PageList<UserDto>(result, pageIndex, pageSize, users.totalItemsCount);
            }
            return null;
        }
        public async Task<List<UserDto>> GetAll()
        {
            var users = await _unitOfWork.UserDashboardRepository.GetAllAsync();
            if (users.Any())
            {
                var result = users.Select(user => new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Country = user.Country,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Orders = user.Orders.Select(o => new UserDashboardOrderDto
                    {
                        Id = o.Id,
                        OrderDate = o.OrderDate,
                        OriginalPrice = o.OrdersDetails
                        .Sum(op => op.Price * op.Quantity),
                        NetPrice = o.OrdersDetails
                        .Sum(op => Math.Round((op.Price - (op.Price * (op.Product.Discount / 100))) * op.Quantity, 0)),
                    })
                });
                return result.ToList();
            }
            return null;
        }

        public async Task<UserDto> GetById(string id)
        {
            var item = await _unitOfWork.UserDashboardRepository.GetByIdAsync(id);
            if (item == null)
            {
                return null;
            }
            else
            {
                var userDto = new UserDto()
                {
                    Id = item.Id,
                    Email = item.Email,
                    UserName = item.UserName,
                    Address = item.Address,
                    Country = item.Country,
                    PhoneNumber= item.PhoneNumber,
                    Orders = item.Orders.Select(o => new UserDashboardOrderDto
                    {
                        Id = o.Id,
                        OrderDate = o.OrderDate,
                        OriginalPrice = o.OrdersDetails
                        .Sum(op => op.Price * op.Quantity),
                        NetPrice = o.OrdersDetails.Sum(op => Math.Round((op.Price - (op.Price * (op.Product.Discount / 100))) * op.Quantity, 0)),

                    })
                };
                return userDto;
            }
        }
        public async Task<ServiceResponse> Delete(string id)
        {
            var user = await _unitOfWork.UserDashboardRepository.GetByIdAsync(id);
            if (user == null)
                throw new ArgumentNullException("id", $"There is no user with id: {id}");

            await _unitOfWork.UserDashboardRepository.DeleteAsync(user);
            await _unitOfWork.SaveAsync();
            return ServiceResponse.Success();
        }

    }
}
