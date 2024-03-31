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
using Microsoft.AspNetCore.Http.HttpResults;

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

        public int CheckAvailability(int productId, int sizeIndex)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                
                SizeEnum size;
                switch (sizeIndex)
                {
                    case 0:
                        size = SizeEnum.Small;
                        break;
                    case 1:
                        size = SizeEnum.Medium;
                        break;
                    case 2:
                        size = SizeEnum.Large;
                        break;
                    case 3:
                        size = SizeEnum.XLarge;
                        break;
                    default:
                        
                        throw new ArgumentException("Invalid size index");
                }

                // Check if the product has the specified size available
                var productWithSizes = _dbContext.Products
                    .Include(p => p.Sizes)
                       .FirstOrDefault(p => p.Id == productId && p.Sizes.Any(s => s.Name == size && s.Quantity > 0));

                return product != null ? product.Sizes.First(s => s.Name == size).Quantity : 0;
            }
            return 0;
        }



        public bool updateProductQuantity(string userId, int productId, int quantity, int sizeIndex)
        {
            
            SizeEnum size;
            switch (sizeIndex)
            {
                case 0:
                    size = SizeEnum.Small;
                    break;
                case 1:
                    size = SizeEnum.Medium;
                    break;
                case 2:
                    size = SizeEnum.Large;
                    break;
                case 3:
                    size = SizeEnum.XLarge;
                    break;
                default:
                    throw new ArgumentException("Invalid size index");
            }

            var cartitem = _dbContext.UserCarts.FirstOrDefault(u => u.UserId == userId && u.ProductId == productId && u.SelectedSize == size);

            if (cartitem != null)
            {
                cartitem.Quantity = quantity;
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
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


        public  async Task DeleteItem(string userId, int productId, int SelectedSize)
        {
            //convert the size index to the size enum
            SizeEnum size;
            switch (SelectedSize)
            {
                case 0:
                    size = SizeEnum.Small;
                    break;
                case 1:
                    size = SizeEnum.Medium;
                    break;
                case 2:
                    size = SizeEnum.Large;
                    break;
                case 3:
                    size = SizeEnum.XLarge;
                    break;
                default:
                    throw new ArgumentException("Invalid size index");
            }

            var product = _dbContext.UserCarts.FirstOrDefault(u => u.UserId == userId && u.ProductId == productId && u.SelectedSize == size);
            if (product != null)
            {
                _dbContext.UserCarts.Remove(product);
                 await _dbContext.SaveChangesAsync();

            }
           




        }

        public async Task AddItemToCart(UserCart userCart)
        {
            //Add the product to the cart
           _dbContext.UserCarts.Add(userCart);
            await _dbContext.SaveChangesAsync();
            
        }
    }
}



