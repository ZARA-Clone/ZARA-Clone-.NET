using AutoMapper;
using E_CommerceProject.Business.Products.Dtos;
using E_CommerceProject.Models;

namespace E_CommerceProject.Business.Products
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<string, ProductImage>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src));

            CreateMap<Product, ProductDto>()
                .ReverseMap()
                .ForMember(c => c.Id, opt =>opt.Ignore());


        }
    }
}
