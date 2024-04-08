using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Models.Models;
using E_CommerceProject.Models;
using E_CommerceProject.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using E_CommerceProject.Business.WishList.Dto;
using Microsoft.EntityFrameworkCore;
using E_CommerceProject.Business.Emails.Dtos;

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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            List<Product> products = _context.WishLists
                .Where(w => w.UserId == userId)
                .Include(w => w.Product)
                .ThenInclude(p => p.ProductImages)
                .Select(w => w.Product)
                .ToList();
            if (products.Count == 0)
            {
                return NotFound("No Products in WishList For This User");
            }

            List<WishlistDto> WLDto = new List<WishlistDto>();
            foreach (var product in products)
            {
                WishlistDto w = new WishlistDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    ImgUrl = product.ProductImages[0].Url,
                    Price = product.Price,
                };
                WLDto.Add(w);
            }
            return Ok(WLDto);



        }
        [Authorize]
        [HttpGet("add")]
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

            return Ok(new {message= "Product added to wishlist successfully" });
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

            return Ok();
        }
    }
}
