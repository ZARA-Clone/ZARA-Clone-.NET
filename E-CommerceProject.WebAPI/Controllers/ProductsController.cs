using E_CommerceProject.Business.Products.Dtos;
using E_CommerceProject.Business.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductsService productsService
            , ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get(string? name, int? brandId, decimal? minPrice
          , decimal? maxPrice, int? rating, int pageIndex = 0, int pageSize = 10)
        {
            _logger.LogInformation($"Get products with brand '{brandId}'," +
               $" min price '{minPrice}',  max price '{maxPrice}', page index '{pageIndex}' and page size '{pageSize}'.");
            var result = await _productsService.Get(name, brandId, minPrice, maxPrice, rating, pageIndex, pageSize);
            _logger.LogInformation($"Get '{result.items.Count}' products from '{result.totalItemsCount}'.");
            return result.items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> Get(int? id)
        {
            if(id == null)
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
    }
}
