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
        public async Task<ActionResult<List<ProductDto>>> Get()
        {
            _logger.LogInformation($"Get all products");
            var items = await _productsService.GetAll();
            return items;
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
