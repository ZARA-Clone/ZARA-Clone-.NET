using E_CommerceProject.Business.Products.Dtos;
using E_CommerceProject.Business.Shared;

namespace E_CommerceProject.Business.Products.Interfaces
{
    public interface IProductsService
    {
        Task<(List<ProductDto> items, int totalItemsCount)> Get(string? name, int? brandId, decimal? minPrice
            , decimal? maxPrice, int? rating, int pageIndex = 0, int pageSize = 10);
        Task<List<ProductDto>> GetAll();
        Task<ProductDto> GetById(int id);
        Task<ServiceResponse> Add(ProductDto productDto);
        Task<ServiceResponse> Edit(int id, ProductDto productDto);
        Task<ServiceResponse> Delete(int id);
    }
}
