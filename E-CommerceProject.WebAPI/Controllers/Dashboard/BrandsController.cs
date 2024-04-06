using AutoMapper;
using E_CommerceProject.Business.Brands.Dtos;
using E_CommerceProject.Business.Brands.Interfaces;
using E_CommerceProject.Business.Shared;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers.Dashboard
{
    [Route("dashboard/api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandsService _brandsService;
        private readonly IMapper _mapper;
        private readonly ILogger<BrandsController> _logger;

        public BrandsController(IBrandsService brandsService
            , IMapper mapper
            , ILogger<BrandsController> logger)
        {
            _brandsService = brandsService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<PageList<BrandReadOnlyDto>>> Get(string? name
            , int? categoryId, int pageIndex = 0, int pageSize = 10)
        {
            _logger.LogInformation($"Get brands with  '{categoryId}'," +
            $", page index '{pageIndex}' and page size '{pageSize}'.");
            var result = await _brandsService.Get(name, categoryId, pageIndex, pageSize);
            _logger.LogInformation($"Get '{result.Items.Count}' products from '{result.TotalCount}'.");
            return result;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandReadOnlyDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BrandReadOnlyDto>> Get(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning($"Invalid id parameter value {id}");
                return BadRequest();
            }
            var item = await _brandsService.GetById(id.Value);
            if (item == null)
            {
                _logger.LogWarning($"There is no brand with id: {id}");
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult<List<BrandReadOnlyDto>>> Get()
        {
            _logger.LogInformation($"Get all brands");
            var items = await _brandsService.GetAll();
            return items;
        }
        [HttpPost]
        public async Task<ActionResult<BrandDto>> Create(BrandDto brand)
        {
            _logger.LogInformation($"Creating new brand.");
            var result = await _brandsService.Add(brand);
            if (result.IsSuccess)
            {
                _logger.LogInformation($"Brand has been added with id {brand.Id}");
                return CreatedAtAction(nameof(Get), new { id = brand.Id }, brand);
            }
            else
            {
                _logger.LogError($"Adding brand not valid with validation errors {result}");
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
                return BadRequest(ModelState);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<BrandDto>> Edit(int id, BrandDto brandDto)
        {
            _logger.LogInformation($"Updating brand with id: {id}");
            if (id != brandDto.Id)
            {
                _logger.LogWarning($"Bad request so id {id} doesn't match object Id {brandDto.Id}");
                return BadRequest();
            }
            try
            {
                var response = await _brandsService.Edit(id, brandDto);
                if (response.IsSuccess)
                {
                    _logger.LogInformation($"Brand with id {id} has been updated");
                    return NoContent();
                }
                else
                {
                    _logger.LogError($"updating brand has been failed with errors {response.Errors}");
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
            _logger.LogInformation($"Deleting brand with id {id}");
            try
            {
                var response = await _brandsService.Delete(id);
                if (response.IsSuccess)
                {
                    _logger.LogWarning($"Brand with id {id} has been deleted");
                    return NoContent();
                }
                else
                {
                    _logger.LogError($"Deleting brand with id {id} has been failed with errors{response.Errors}");
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
