using E_CommerceProject.Business.Brands.Dtos;
using E_CommerceProject.Business.Shared;

namespace E_CommerceProject.Business.Brands.Interfaces
{
    public interface IBrandsService
    {
        Task<PageList<BrandReadOnlyDto>> Get(string? name
            , int? categoryId, int pageIndex = 0, int pageSize = 10);
        Task<BrandReadOnlyDto> GetById(int id);
        Task<List<BrandReadOnlyDto>> GetAll();
        Task<ServiceResponse> Add(BrandDto brandDto);
        Task<ServiceResponse> Edit(int id, BrandDto brandDto);
        Task<ServiceResponse> Delete(int id);
    }
}
