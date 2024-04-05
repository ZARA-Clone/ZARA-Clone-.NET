using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Models.Models;
using E_CommerceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using E_CommerceProject.Business.Wishlist.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly ECommerceContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WishListController(ECommerceContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddToWishList(int productId)

        {
            var UserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(UserID);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            var existingWishlist = _context.WishLists.FirstOrDefault(p => p.UserId == UserID && p.ProductId == product.Id);
            if (existingWishlist != null)
            {
                return BadRequest("Product already existing in your wishlist");
            }

            var wishListItem = new WishList
            {
                UserId = UserID,
                ProductId = productId,
            };

            _context.WishLists.Add(wishListItem);
            _context.SaveChanges();

            return Ok("Product added to wishlist successfully");
        }


        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteFromWishList(int productId)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var wishListItem = _context.WishLists.FirstOrDefault(w => w.UserId == userId && w.ProductId == productId);
            if (wishListItem == null)
            {
                return NotFound("Product not found in the wishlist");
            }

            _context.WishLists.Remove(wishListItem);
            _context.SaveChanges();

            return Ok("Product removed from wishlist successfully");
        }
    }
}
