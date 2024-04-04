using E_CommerceProject.Business.Cart.Dtos;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Repositories.cart;
using E_CommerceProject.Infrastructure.Repositories.User;
using E_CommerceProject.Models;
using E_CommerceProject.Models.Enums;
using E_CommerceProject.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using System.Security.Claims;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ECommerceContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DecodeTokenRepository decode;

        public CartController(ICartRepository cartRepository, ECommerceContext _context, UserManager<ApplicationUser> userManager, DecodeTokenRepository _decode)
        {
            _cartRepository = cartRepository;
            context = _context;
            this.userManager = userManager;
            decode = _decode;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartItems([FromRoute] string userId)
        {
            #region testing

            // Add Dummy User Cart
            List<UserCart> ListUserCart = new List<UserCart> {
                new UserCart { ProductId = 1, UserId = userId, Quantity = 1, SelectedSize = Size.Small },
                new UserCart { ProductId = 1, UserId = userId, Quantity = 1, SelectedSize = Size.Medium },
                new UserCart { ProductId = 1, UserId = userId, Quantity = 1, SelectedSize = Size.Large },

            };
            // push to Db if User Cart is Empty
            if (_cartRepository.GetCartItemsAsync(userId).Result.Count == 0)
            {
                foreach (var item in ListUserCart)
                {
                    await _cartRepository.AddItemToCart(item);
                }
            }
            #endregion

            var cartItems = await _cartRepository.GetCartItemsAsync(userId);

            // Create a list to store CartItemDto objects
            List<CartItemDto> cartItemsDto = new List<CartItemDto>();

            // Loop through each cart item and create a CartItemDto object for each
            foreach (var cartItem in cartItems)
            {
                // Create a new CartItemDto object and populate its properties
                CartItemDto cartItemDto = new CartItemDto
                {
                    Id = cartItem.ProductId,
                    Title = cartItem.Product.Name,
                    Price = cartItem.Product.Price,
                    Image = cartItem.Product.ProductImages[0].Url,
                    Quantity = cartItem.Quantity,
                    Size = cartItem.SelectedSize
                };

                // Add the CartItemDto object to the list
                cartItemsDto.Add(cartItemDto);
            }

            // Return the list of CartItemDto objects
            return Ok(cartItemsDto);
        }

        [HttpGet("check-availability/{productId}/{size}")]
        public IActionResult CheckAvailability(int productId, int size)
        {
            int availableQuantity = _cartRepository.CheckAvailability(productId, size);
            return Ok(availableQuantity);
        }



        [HttpPut("update-quantity/{userId}/{productId}")]
        public IActionResult updateProductQuantity(string userId, int productId, int quantity, int size)
        {

            bool success = _cartRepository.updateProductQuantity(userId, productId, quantity, size);
            if (success)
            {
                return Ok("Quantity updated successfully in the database");
            }
            else
            {
                return BadRequest("Error updating quantity in the database");
            }
        }

        [HttpDelete("delete-item/{userId}/{productId}/{size}")]
        public async Task<IActionResult> DeleteItemBySize(string userId, int productId, int size)
        {

            try
            {
                await _cartRepository.DeleteItem(userId, productId, size);
                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart(int productId, int size)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var product = context.Products
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound();
                }

            var existingCartItem = context.UserCarts.FirstOrDefault(cart =>
                cart.UserId == userId && cart.ProductId == productId && cart.SelectedSize == (Size)size);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity++;
                context.SaveChanges();
                return Ok();
            }

            var userCart = new UserCart
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = 1,
                    SelectedSize = (Size)size,
                };

                context.UserCarts.Add(userCart);
                context.SaveChanges();

                return Ok();
        }
    }
}





