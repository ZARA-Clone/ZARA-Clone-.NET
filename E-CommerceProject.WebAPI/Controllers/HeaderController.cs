using E_CommerceProject.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeaderController : ControllerBase
    {
        private readonly ECommerceContext _context;
        public HeaderController(ECommerceContext context) {
            _context = context;
        }
        [HttpGet("getallcat")]
        public IActionResult GetAllCategories ()
        {
            var allCategories = _context.Categories
                                .Select(c => new
                                {c.Id,c.Name}).ToList();
            return Ok(allCategories);
        }

        [HttpGet("getallbrand")]
        public IActionResult GetAllBrands(int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                return BadRequest("Category not found");
            }

            var allbrands = _context.Brands.Where(b => b.CategoryId == categoryId)
                                           .Select(b => new
                                           {b.Id, b.Name, b.CategoryId}).ToList();
            return Ok(allbrands);
        }
    }
}
