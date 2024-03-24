using AutoMapper;
using E_CommerceProject.Business.Brands.Dtos;
using E_CommerceProject.Business.Brands.Interfaces;
using E_CommerceProject.Business.Shared;
using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace E_CommerceProject.Business.Brands
{
    public class BrandsService : IBrandsService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<Brand> _validator;

        public BrandsService(IUnitOfWorkAsync unitOfWork
            , ILogger<BrandsService> logger
            , IMapper mapper
            , IValidator<Brand> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BrandReadOnlyDto> GetById(int id)
        {
            if(id < 0) throw new ArgumentNullException("id", "id can not be negative");
            var item = await _unitOfWork.BrandRepository.GetByIdAsync(id);
            if(item == null)
                throw new ArgumentNullException("id", $"There is no brand with id: {id}");

            return _mapper.Map<BrandReadOnlyDto>(item);
        }

        public async Task<List<BrandReadOnlyDto>> GetAll()
        {
            var items = await _unitOfWork.BrandRepository.GetAllAsync();
            return _mapper.Map<List<BrandReadOnlyDto>>(items);
        }

        public async Task<ServiceResponse> Add(BrandDto brandDto)
        {
            var newBrand = _mapper.Map<Brand>(brandDto);
            
            //validation
            var validationResult = await _validator.ValidateAsync(newBrand);
            if (!validationResult.IsValid)
                return ServiceResponse.Fail(validationResult.Errors);

            await _unitOfWork.BrandRepository.AddAsync(newBrand);
            await _unitOfWork.SaveAsync();
            brandDto.Id = newBrand.Id;
            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> Edit(int id, BrandDto brandDto)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("id", "id cannot be negative or empty");
            if (brandDto == null) throw new ArgumentNullException("brandDto", "brand cannot be null");

            var  brand = await _unitOfWork.BrandRepository.GetByIdAsync(id);
            if (brand == null)
                throw new ArgumentNullException("id", $"There is no brand with id: {id}");

            _mapper.Map(brandDto, brand);
            var validationResult = await _validator.ValidateAsync(brand);
            if (!validationResult.IsValid)
                return ServiceResponse.Fail(validationResult.Errors);

            await _unitOfWork.SaveAsync();
            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var brand = await _unitOfWork.BrandRepository.GetByIdAsync(id);
            if (brand == null)
                throw new ArgumentNullException("id", $"There is no brand with id: {id}");

            await _unitOfWork.BrandRepository.DeleteAsync(brand);
            await _unitOfWork.SaveAsync();
            return ServiceResponse.Success();
        }
    }
}
