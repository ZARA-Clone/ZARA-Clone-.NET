using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeaderController : ControllerBase
    {
        private readonly ECommerceContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public HeaderController(ECommerceContext context,UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
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
        [HttpGet("getcartlength")]
        public async Task<IActionResult> GetCartLength()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var userCarts = _context.UserCarts.Where(c => c.UserId == userId).ToList();

            int cartLength = userCarts.Sum(cart => cart.Quantity);

            return Ok(new { cartLength });
        }
    }
}
