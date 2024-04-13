using E_CommerceProject.Business.Shared;
using E_CommerceProject.Business.Users.Dtos;
namespace E_CommerceProject.Business.Users.Interfaces
{
    public interface IUserService
    {
        Task<PageList<UserDto>> Get(int pageIndex = 0, int pageSize = 10);
        Task<UserDto> GetById(string id);
        Task<List<UserDto>> GetAll();
        Task<ServiceResponse> Delete(string id);


    }
}
