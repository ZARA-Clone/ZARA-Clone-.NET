using AutoMapper;
using E_CommerceProject.Business.Brands.Dtos;
using E_CommerceProject.Models;

namespace E_CommerceProject.Business.Brands
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>()
            .ReverseMap()
            .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}
