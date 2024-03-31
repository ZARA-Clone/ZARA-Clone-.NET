using E_CommerceProject.Infrastructure.Repositories.cart;
using E_CommerceProject.Models;
using E_CommerceProject.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers
{

    public class CartItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        //public RatingDto ?Rating { get; set; }

        public int Quantity { get; set; }

        public SizeEnum Size { get; set; }

    }

    //public class RatingDto
    //{
    //    public decimal Rate { get; set; }
    //    public int Count { get; set; }
    //}


    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {









        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartItems([FromRoute] string userId)
        {
            #region testing
          
            // Add Dummy User Cart
            List<UserCart> ListUserCart = new List<UserCart> {
                new UserCart { ProductId = 1, UserId = userId, Quantity = 1, SelectedSize = SizeEnum.Small },
                new UserCart { ProductId = 1, UserId = userId, Quantity = 1, SelectedSize = SizeEnum.Medium },
                new UserCart { ProductId = 1, UserId = userId, Quantity = 1, SelectedSize = SizeEnum.Large },

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



        //[HttpPost("update")]
        //public async Task<IActionResult> UpdateCartItemQuantity(string userId, int productId, SizeEnum size, int quantity)
        //{
        //    var success = await _cartRepository.UpdateQuantityAsync(userId, productId, size, quantity);
        //    if (success)
        //        return Ok("Quantity updated successfully.");
        //    else
        //        return BadRequest("Failed to update quantity. The selected size might not be available in the stock.");
        //}


        //[HttpDelete("delete")]
        //    public async Task<IActionResult> DeleteCartItem(string userId, int productId)
        //    {
        //        var success = await _cartRepository.DeleteCartItemAsync(userId, productId);
        //        if (success)
        //            return Ok("Item deleted successfully.");
        //        else
        //            return BadRequest("Failed to delete item.");
        //    }






        [HttpGet("check-availability/{productId}/{size}")]
        public IActionResult CheckAvailability(int productId,int size)
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



        //    delete-item/${userId
        ///${itemId


       [HttpDelete("delete-item/{userId}/{productId}/{size}")]
       public async Task<IActionResult> DeleteItemBySize(string userId,int productId, int size)
        {
           

            try
            {
                await _cartRepository.DeleteItem(userId, productId, size);
                return Ok();
            }
            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }





        }











    }
    }





