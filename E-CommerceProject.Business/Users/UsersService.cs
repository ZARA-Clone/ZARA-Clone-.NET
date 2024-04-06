using AutoMapper;
using E_CommerceProject.Business.Shared;
using E_CommerceProject.Business.Users.Dtos;
using E_CommerceProject.Business.Users.Interfaces;
using E_CommerceProject.Infrastructure.Core.Base;

namespace E_CommerceProject.Business.Users
{
    public class UsersService : IUserService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;

        public UsersService(IUnitOfWorkAsync unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _unitOfWork.UserDashboardRepository.GetAllAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetById(string id)
        {
            var item = await _unitOfWork.UserDashboardRepository.GetByIdAsync(id);
            return item == null ? null : _mapper.Map<UserDto>(item);
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
