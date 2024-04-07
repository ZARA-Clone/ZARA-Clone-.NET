using E_CommerceProject.Business.AddToWishlist.Dto;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Repositories.AddtoWishlist;
using E_CommerceProject.Models;
using E_CommerceProject.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddToWishlistController : ControllerBase
    {
        private readonly IaddToWishlist _wishlistRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public AddToWishlistController(IaddToWishlist wishlistRepository, UserManager<ApplicationUser> userManager)
        {
            _wishlistRepository = wishlistRepository;
            this.userManager = userManager;
        }



        [HttpPost]
        public async Task<ActionResult<AddtoWishlist>> addtowishlist([FromBody]AddToWishlistDto AddToWishlistDto)
        {

            var user = await userManager.GetUserAsync(User);
         
            var wishlist = new WishList
            {
                UserId = user.Id,
                ProductId = AddToWishlistDto.ProductId
            };
            var addedItem = await _wishlistRepository.AddToWishlist(wishlist);
            return Ok(addedItem);

        }


        [HttpDelete]
        public async Task removewishlistitem([FromQuery] int ProductId, [FromQuery] string UserId)
        {
            var user = await userManager.GetUserAsync(User);
            var wishlist = new WishList
            {
                UserId = user.Id,
                ProductId = ProductId
            };
           await _wishlistRepository.removefromwishlist(wishlist);
           

        }


        [HttpGet]
        [Route("checkwish/{itemId}/{userId}")]
        public async Task<IActionResult> checkwishlist(int itemId , string userId) {

            var user = await userManager.GetUserAsync(User);
            userId = user.Id;

            bool iswishlist=_wishlistRepository.IsWishlist(itemId, userId);

         

            return Ok(iswishlist);  
        }




    }



}
