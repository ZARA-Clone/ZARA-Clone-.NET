using E_CommerceProject.Business.Shared;
using E_CommerceProject.Business.Users.Dtos;
namespace E_CommerceProject.Business.Users.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(string id);
        Task<List<UserDto>> GetAll();
        Task<ServiceResponse> Delete(string id);


    }
}
