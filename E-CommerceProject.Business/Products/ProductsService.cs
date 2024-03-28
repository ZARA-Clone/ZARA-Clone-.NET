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

        public async Task<ServiceResponse> Add(AddProductDto productDto)
        {
            var newProduct = _mapper.Map<Product>(productDto);
            // ToDo: handle images
            foreach (var item in productDto.ImageUrls)
            {
                newProduct.ProductImages.Add(new ProductImage { Url = item });
            }

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
            //product.ProductImages.Clear();
            // ToDo: handle images
            foreach (var item in productDto.ImageUrls)
            {
                product.ProductImages.Add(new ProductImage { Url = item });
            }
            var validationResult = await _validator.ValidateAsync(product);
            if (!validationResult.IsValid)
                return ServiceResponse.Fail(validationResult.Errors);

            await _unitOfWork.SaveAsync();
            return ServiceResponse.Success();
        }

        public async Task<ServiceResponse> Delete(int id)
        {
            var product = await _unitOfWork.ProductsRepository.GetByIdAsync(id);
            if (product == null)
                throw new ArgumentNullException("id", $"There is no product with id: {id}");

            await _unitOfWork.ProductsRepository.DeleteAsync(product);
            await _unitOfWork.SaveAsync();
            return ServiceResponse.Success();
        }

        public async Task<PageList<ProductDto>> Get(string? name, int? brandId, decimal? minPrice
            , decimal? maxPrice, int pageIndex = 0, int pageSize = 10)
        {
            _logger.LogInformation($"Get products with brand '{brandId}'," +
                $" min price '{minPrice}',  max price '{maxPrice}', page index '{pageIndex}' and page size '{pageSize}'.");
            var result = await _unitOfWork.ProductsRepository.Get(name, brandId, minPrice, maxPrice, pageIndex, pageSize);
            _logger.LogInformation($"Get '{result.items.Count}' products from '{result.totalItemsCount}'.");
            return new PageList<ProductDto>(_mapper.Map<List<ProductDto>>(result.items), pageIndex, pageSize, result.totalItemsCount);
        }
    }
}
