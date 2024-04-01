using E_CommerceProject.Business.Products.Dtos;
using E_CommerceProject.Business.Shared;

namespace E_CommerceProject.Business.Products.Interfaces
{
    public interface IProductsService
    {
        Task<PageList<ProductDto>> Get(string? name, int? brandId, decimal? minPrice
            , decimal? maxPrice, int pageIndex = 0, int pageSize = 10);
        Task<List<ProductDto>> GetAll();
        Task<ProductDto> GetById(int id);
        Task<ServiceResponse> Add(AddProductDto productDto);
        Task<ServiceResponse> Edit(int id, ProductDto productDto);
        Task<ServiceResponse> Delete(int id);
    }
}
