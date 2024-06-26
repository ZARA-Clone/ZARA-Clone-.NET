﻿using E_CommerceProject.Business.Products.Dtos;
using E_CommerceProject.Business.Products.Interfaces;
using E_CommerceProject.Business.Shared;
using E_CommerceProject.Business.WishList.Dto;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Models;
using E_CommerceProject.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly ILogger<ProductsController> _logger;
        private readonly ECommerceContext _context;

        public ProductsController(IProductsService productsService
            , ILogger<ProductsController> logger, ECommerceContext context)
        {
            _productsService = productsService;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PageList<ProductDto>>> Get(string? name, int? brandId, decimal? minPrice
          , decimal? maxPrice, int pageIndex = 0, int pageSize = 10)
        {
            _logger.LogInformation($"Get products with brand '{brandId}'," +
               $" min price '{minPrice}',  max price '{maxPrice}', page index '{pageIndex}' and page size '{pageSize}'.");
            var result = await _productsService.Get(name, brandId, minPrice, maxPrice, pageIndex, pageSize);
            _logger.LogInformation($"Get '{result.Items.Count}' products from '{result.TotalCount}'.");
            return result;
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<ProductDto>> Get(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning($"Invalid id parameter value {id}");
                return BadRequest();
            }
            var item = await _productsService.GetById(id.Value);
            if (item == null)
            {
                _logger.LogWarning($"There is no product with id: {id}");
                return NotFound();
            }
            return Ok(item);
        }


        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var product = _context.Products.Include(p => p.ProductSizes)
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            List<string> imageUrls = product.ProductImages
                .Select(img => img.Url)
                .ToList();

            List<SizeQuantityDto> sizes = product.ProductSizes
                .Select(size => new SizeQuantityDto
                {
                    Size = size.Size,
                    Quantity = size.Quantity
                })
                .ToList();
            ProductDetailsDto PD = new ProductDetailsDto()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Images = imageUrls,
                Sizes = sizes,
                Discount = product.Discount,
                BrandId = product.BrandId
            };
            return Ok(PD);
        }
        [HttpGet("brand/{id}")]
public ActionResult GetByBrandId(int id)
        {
            List<Product> products = _context.Products.Include(p => p.ProductSizes)
                .Include(p => p.ProductImages)
                .Where(p => p.BrandId == id).ToList();
            if (products == null)
            {
                return NotFound();
            }
        

            List<ProductBrowseDto> PBD = new List<ProductBrowseDto>();

            foreach (var product in products)
            {
                List<SizeQuantityDto> sizes = product.ProductSizes
                .Select(size => new SizeQuantityDto
                {
                    Size = size.Size,
                    Quantity = size.Quantity
                })
                .ToList();
                var productImages = product.ProductImages.Select(img => img.Url).ToList();

                ProductBrowseDto PB = new ProductBrowseDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ImgUrl = productImages[0],
                    BrandId = product.BrandId,
                    Sizes = sizes

                };
                PBD.Add(PB);

            }
            return Ok(PBD);

        }


        [HttpGet("getallproducts")]
        public ActionResult GetAllProducts()
        {
            List<Product> products = _context.Products.Include(p => p.ProductSizes)
                .Include(p => p.ProductImages)
                .ToList();
            if (products == null)
            {
                return NotFound();
            }


            List<ProductBrowseDto> PBD = new List<ProductBrowseDto>();

            foreach (var product in products)
            {
                List<SizeQuantityDto> sizes = product.ProductSizes
                .Select(size => new SizeQuantityDto
                {
                    Size = size.Size,
                    Quantity = size.Quantity
                })
                .ToList();
                var productImages = product.ProductImages.Select(img => img.Url).ToList();

                ProductBrowseDto PB = new ProductBrowseDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ImgUrl = productImages[0],
                    BrandId = product.BrandId,
                    Sizes = sizes

                };
                PBD.Add(PB);

            }
            return Ok(PBD);

        }



    }
}
