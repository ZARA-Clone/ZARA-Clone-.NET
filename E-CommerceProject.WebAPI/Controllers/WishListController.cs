﻿using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Models;
using E_CommerceProject.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using E_CommerceProject.Business.WishList.Dto;
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

        [HttpPost("add")]
            public async Task <IActionResult>  AddToWishList( int productId)
         
            {
            var UserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(UserID);

           // var user = _context.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                var product = _context.Products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    return NotFound("Product not found");
                }
              var existingWishlist= _context.WishLists.FirstOrDefault(p => p.UserId == user.Id &&  p.ProductId==product.Id);
            if(existingWishlist != null) {
                return BadRequest("Product already existing in your wishlist");
                    }

            var wishListItem = new WishList
                {
                    UserId = UserID,
                    ProductId = productId,
                    User = user,
                    Product = product
                };

                _context.WishLists.Add(wishListItem);
                _context.SaveChanges();

                return Ok("Product added to wishlist successfully");
            }

            [HttpDelete("delete")]


            public async Task  <IActionResult> DeleteFromWishList( int productId)

            {
            var UserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(UserID);


            var wishListItem = _context.WishLists.FirstOrDefault(w => w.UserId == UserID && w.ProductId == productId);
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
