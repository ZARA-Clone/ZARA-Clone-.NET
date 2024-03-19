using AutoMapper;
using E_CommerceProject.Business.Brands.Dtos;
using E_CommerceProject.Business.Brands.Interfaces;
using E_CommerceProject.Infrastructure.Core.Base;
using Microsoft.Extensions.Logging;

namespace E_CommerceProject.Business.Brands
{
    public class BrandsService : IBrandsService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly ILogger<BrandsService> _logger;
        private readonly IMapper _mapper;

        public BrandsService(IUnitOfWorkAsync unitOfWork
            , ILogger<BrandsService> logger
            , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BrandDto> GetById(int id)
        {
            var item = await _unitOfWork.BrandRepository.GetByIdAsync(id);
            if(item == null)
            {
                return null;
            }
            return _mapper.Map<BrandDto>(item);
        }

        public async Task<List<BrandDto>> GetAll()
        {
            var items = await _unitOfWork.BrandRepository.GetAllAsync();
            return _mapper.Map<List<BrandDto>>(items);
        }
    }
}
