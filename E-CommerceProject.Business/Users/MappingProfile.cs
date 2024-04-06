using AutoMapper;
using E_CommerceProject.Business.Users.Dtos;
using E_CommerceProject.Models.Models;

namespace E_CommerceProject.Business.Users
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ReverseMap();

        }
    }
}
