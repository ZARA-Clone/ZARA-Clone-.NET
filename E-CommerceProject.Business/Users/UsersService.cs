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
                    Orders = item.Orders.Select(o => new UserDashboardOrderDto
                    {
                        Id = o.Id,
                        OrderDate = o.OrderDate,
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
