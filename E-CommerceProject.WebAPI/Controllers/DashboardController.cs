using E_CommerceProject.Business.Products.Dtos;
using E_CommerceProject.Business.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IProductsService productsService
            , ILogger<DashboardController> logger)
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
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create(ProductDto product)
        {
            _logger.LogInformation($"Creating new product.");
            var result = await _productsService.Add(product);
            if (result.IsSuccess)
            {
                _logger.LogInformation($"Product has been added with id {product.Id}");
                return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
            }
            else
            {
                _logger.LogError($"Adding product not valid with validation errors {result}");
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Edit(int id, ProductDto productDto)
        {
            _logger.LogInformation($"Updating product with id: {id}");
            if (id != productDto.Id)
            {
                _logger.LogWarning($"Bad request so id {id} doesn't match object Id {productDto.Id}");
                return BadRequest();
            }
            try
            {
                var response = await _productsService.Edit(id, productDto);
                if (response.IsSuccess)
                {
                    _logger.LogInformation($"product with id {id} has been updated");
                    return NoContent();
                }
                else
                {
                    _logger.LogError($"updating product has been failed with errors {response.Errors}");
                    foreach (var error in response.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return BadRequest(ModelState);
                }
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation($"Deleting product with id {id}");
            try
            {
                var response = await _productsService.Delete(id);
                if (response.IsSuccess)
                {
                    _logger.LogWarning($"Product with id {id} has been deleted");
                    return NoContent();
                }
                else
                {
                    _logger.LogError($"Deleting product with id {id} has been failed with errors{response.Errors}");
                    foreach (var error in response.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                    return BadRequest(ModelState);
                }
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound();
            }
        }
    }
}
