using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_CommerceProject.Models.Enums;
using E_CommerceProject.Infrastructure.Repositories.cart;
using E_CommerceProject.Models.Models;

namespace E_CommerceProject.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartRepository(ECommerceContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<List<UserCart>> GetCartItemsAsync(string userId)
        {
            return await _dbContext.UserCarts
                                   .Include(u => u.Product).ThenInclude(f => f.ProductImages)
                                   .Where(c => c.UserId == userId)// &&
                                   //            _dbContext.Products.Any(p => p.Id == c.ProductId &&
                                   //                                           p.Sizes.Any(s => s.Name == c.SelectedSize && s.Quantity > 0)))
                                   .ToListAsync();
        }


        public async Task<bool> UpdateQuantityAsync(string userId, int productId, SizeEnum size, int quantity)
        {
            // Get the cart item
            var cartItem = await _dbContext.UserCarts.FirstOrDefaultAsync(u => u.ProductId == productId && u.UserId == userId && u.SelectedSize == size);

            // Check if the cart item exists
            if (cartItem != null)
            {
                // Get the product
                var product = await _dbContext.Products.Include(p => p.Sizes).FirstOrDefaultAsync(p => p.Id == productId);

                // Check if the product exists
                if (product != null)
                {
                    // Find the size in the product's available sizes
                    var productSize = product.Sizes.FirstOrDefault(s => s.Name == size);

                    // Check if the size exists and if the quantity requested is available
                    if (productSize != null && productSize.Quantity >= quantity)
                    {
                        // Update the quantity of the existing item
                        cartItem.Quantity += quantity;

                        // Decrease the quantity of the size in the product's available sizes
                    //    productSize.Quantity -= quantity;

                        // Save changes to the database
                        await _dbContext.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        // The selected size is not available in the stock
                        return false;
                    }
                }
                else
                {
                    // Product not found
                    return false;
                }
            }
            else
            {
                // Add a new item to the cart
                _dbContext.UserCarts.Add(new UserCart { UserId = userId, ProductId = productId, SelectedSize = size, Quantity = quantity });

                // Save changes to the database
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }


        public async Task<bool> DeleteCartItemAsync(string userId, int productId)
        {
            var cartItem = await _dbContext.UserCarts.FirstOrDefaultAsync(u => u.ProductId == productId && u.UserId == userId);
            if (cartItem != null)
            {
                _dbContext.UserCarts.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}



