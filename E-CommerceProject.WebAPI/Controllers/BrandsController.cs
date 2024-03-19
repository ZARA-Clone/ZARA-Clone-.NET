using AutoMapper;
using E_CommerceProject.Business.Brands.Dtos;
using E_CommerceProject.Business.Brands.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BrandDto>> Get(int? id)
        {
            if(id == null)
            {
                _logger.LogWarning($"Invalid id parameter value {id}");
                return BadRequest();
            }
            var item = await _brandsService.GetById(id.Value);
            if(item == null)
            {
                _logger.LogWarning($"There is no brand with id: {id}");
                return NotFound();
            }
            return Ok(item);
        }
    }
}
