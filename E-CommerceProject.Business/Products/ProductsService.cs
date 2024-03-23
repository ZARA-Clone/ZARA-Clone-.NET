using AutoMapper;
using E_CommerceProject.Business.Products.Dtos;
using E_CommerceProject.Business.Products.Interfaces;
using E_CommerceProject.Business.Shared;
using E_CommerceProject.Infrastructure.Core.Base;
using E_CommerceProject.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace E_CommerceProject.Business.Products
{
    public class ProductsService : IProductsService
    {
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsService> _logger;
        private readonly IValidator<Product> _validator;

        public ProductsService(IUnitOfWorkAsync unitOfWork
            , IMapper mapper
            , ILogger<ProductsService> logger
            , IValidator<Product> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var items = await _unitOfWork.ProductsRepository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(items);
        }

        public async Task<ProductDto> GetById(int id)
        {
            if (id < 0) throw new ArgumentNullException("id", "id can not be negative");
            var item = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(item);
        }

        public async Task<ServiceResponse> Add(ProductDto productDto)
        {
            var newProduct = _mapper.Map<Product>(productDto);
            // ToDo: handle images

            //validation
            var validationResult = await _validator.ValidateAsync(newProduct);
            if (!validationResult.IsValid)
                return ServiceResponse.Fail(validationResult.Errors);

            await _unitOfWork.ProductsRepository.AddAsync(newProduct);
            await _unitOfWork.SaveAsync();
            productDto.Id = newProduct.Id;
            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> Edit(int id, ProductDto productDto)
        {
            if (id <= 0) throw new ArgumentOutOfRangeException("id", "id cannot be negative or empty");
            if (productDto == null) throw new ArgumentNullException("productDto", "Product cannot be null");
            
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (product == null)
                throw new ArgumentNullException("id", $"There is no product with id: {id}");

            _mapper.Map(productDto, product);
            var validationResult  = await  _validator.ValidateAsync(product);
            if (!validationResult.IsValid)
                return ServiceResponse.Fail(validationResult.Errors);

            await _unitOfWork.SaveAsync();
            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if(product == null)
                throw new ArgumentNullException("id", $"There is no product with id: {id}");

            await _unitOfWork.ProductsRepository.DeleteAsync(product);
            await _unitOfWork.SaveAsync();
            return ServiceResponse.Success();
        }

        public async Task<(List<ProductDto> items, int totalItemsCount)> Get(string? name, int? brandId, decimal? minPrice
            , decimal? maxPrice, int? rating, int pageIndex = 0, int pageSize = 10)
        {
            _logger.LogInformation($"Get products with brand '{brandId}'," +
                $" min price '{minPrice}',  max price '{maxPrice}', page index '{pageIndex}' and page size '{pageSize}'.");
            var result = await _unitOfWork.ProductsRepository.Get(name, brandId, minPrice, maxPrice, rating, pageIndex, pageSize);
            _logger.LogInformation($"Get '{result.items.Count}' products from '{result.totalItemsCount}'.");
            return (_mapper.Map<List<ProductDto>>(result.items), result.totalItemsCount);
        }
    }
}
