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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
 
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ECommerceContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ICartRepository cartRepository, ECommerceContext _context, UserManager<ApplicationUser> userManager)
        {
            _cartRepository = cartRepository;
            context = _context;
            this.userManager = userManager;
          
        }
        
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartItems([FromRoute] string userId)
        {

            var user = await userManager.GetUserAsync(User);
            userId = user.Id;
            var cartItems = await _cartRepository.GetCartItemsAsync(userId);




            //-----------to decrease the quantity in cart if the stock decreased-------

            foreach (var cartItem in cartItems)
            {
                // Calculate available stock quantity for the product and size
                int availableStock = _cartRepository.CheckAvailability(cartItem.ProductId, (int)cartItem.SelectedSize);

             
                int quantity = Math.Min(cartItem.Quantity, availableStock);

                // Update the quantity in the cart if necessary
                if (quantity == 0)
                {
                    // If the quantity in stock is zero, remove the item from the cart
                    await _cartRepository.DeleteItem(userId, cartItem.ProductId, (int)cartItem.SelectedSize);
                }
                else
                {
                    
                    _cartRepository.updateProductQuantity(userId, cartItem.ProductId, quantity, (int)cartItem.SelectedSize);
                }
            }

            // Now retrieve the updated cart items from the database
            cartItems = await _cartRepository.GetCartItemsAsync(userId);




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
                    Size = cartItem.SelectedSize,
                    Description = cartItem.Product.Description
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
        public async Task<IActionResult> updateProductQuantity(string userId, int productId, int quantity, int size)
        {
            var user = await userManager.GetUserAsync(User);
            userId = user.Id;
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

            var user = await userManager.GetUserAsync(User);
            userId = user.Id;
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
        [Authorize]
        [HttpGet("AddToCart")]
        public async Task<IActionResult> AddToCart(int productId, int size)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var product = context.Products
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            var user = await userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found");
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





