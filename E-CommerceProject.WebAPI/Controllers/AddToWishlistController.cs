﻿using E_CommerceProject.Business.AddToWishlist.Dto;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Repositories.AddtoWishlist;
using E_CommerceProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddToWishlistController : ControllerBase
    {
        private readonly IaddToWishlist _wishlistRepository;

        public AddToWishlistController(IaddToWishlist wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }



        [HttpPost]
        public async Task<ActionResult<AddtoWishlist>> addtowishlist([FromBody]AddToWishlistDto AddToWishlistDto)
        {
            var wishlist = new WishList
            {
                UserId = AddToWishlistDto.UserId,
                ProductId = AddToWishlistDto.ProductId
            };
            var addedItem = await _wishlistRepository.AddToWishlist(wishlist);
            return Ok(addedItem);

        }


        [HttpDelete]
        public async Task removewishlistitem([FromQuery] int ProductId, [FromQuery] string UserId)
        {
            var wishlist = new WishList
            {
                UserId = UserId,
                ProductId = ProductId
            };
           await _wishlistRepository.removefromwishlist(wishlist);
           

        }


        [HttpGet]
        [Route("checkwish/{itemId}/{userId}")]
        public IActionResult checkwishlist(int itemId , string userId) {
        
            bool iswishlist=_wishlistRepository.IsWishlist(itemId, userId); ;
            
            return Ok(iswishlist);  
        }




    }



}
