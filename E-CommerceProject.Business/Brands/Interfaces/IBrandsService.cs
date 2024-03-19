using E_CommerceProject.Business.Brands.Dtos;

namespace E_CommerceProject.Business.Brands.Interfaces
{
    public interface IBrandsService
    {
        Task<BrandDto> GetById(int id);
        Task<List<BrandDto>> GetAll();
    }
}
