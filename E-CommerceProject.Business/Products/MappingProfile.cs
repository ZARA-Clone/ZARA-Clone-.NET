using AutoMapper;
using E_CommerceProject.Business.Products.Dtos;
using E_CommerceProject.Models;

namespace E_CommerceProject.Business.Products
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(c => c.Sizes, opt =>
                    opt.MapFrom(c => c.ProductSizes.Select(s => new KeyValuePair<string, int>(s.Size.ToString(), s.Quantity))))
                .ForMember(c => c.ImageUrls, opt => opt.MapFrom(c => c.ProductImages.Select(img => img.Url)))
                .ForMember(c => c.BrandName, opt => opt.MapFrom(c => c.Brand.Name))
                .ReverseMap();

            CreateMap<Product, AddProductDto>()
                .ReverseMap();

        }
    }
}
