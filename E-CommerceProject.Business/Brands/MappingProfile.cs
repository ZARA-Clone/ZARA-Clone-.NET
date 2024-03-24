using AutoMapper;
using E_CommerceProject.Business.Brands.Dtos;
using E_CommerceProject.Models;

namespace E_CommerceProject.Business.Brands
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandReadOnlyDto>()
                .ForMember(c => c.NoOfProducts, opt => opt.MapFrom(src => src.Products.Count()));

            CreateMap<Brand, BrandDto>()
                .ReverseMap();

        }
    }
}
