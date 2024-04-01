using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.Repositories.cart;
using E_CommerceProject.Models;
using E_CommerceProject.Models.Enums;
using E_CommerceProject.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

                Size size;
                switch (sizeIndex)
                {
                    case 0:
                        size = Size.Small;
                        break;
                    case 1:
                        size = Size.Medium;
                        break;
                    case 2:
                        size = Size.Large;
                        break;
                    case 3:
                        size = Size.XLarge;
                        break;
                    default:

                        throw new ArgumentException("Invalid size index");
                }

                // Check if the product has the specified size available
                var productWithSizes = _dbContext.Products
                    .Include(p => p.ProductSizes)
                       .FirstOrDefault(p => p.Id == productId && p.ProductSizes.Any(s => s.Size == size && s.Quantity > 0));

                return product != null ? product.ProductSizes.First(s => s.Size == size).Quantity : 0;
            }
            return 0;
        }

        public bool updateProductQuantity(string userId, int productId, int quantity, int sizeIndex)
        {

            Size size;
            switch (sizeIndex)
            {
                case 0:
                    size = Size.Small;
                    break;
                case 1:
                    size = Size.Medium;
                    break;
                case 2:
                    size = Size.Large;
                    break;
                case 3:
                    size = Size.XLarge;
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
                        .Include(u => u.Product)
                            .ThenInclude(f => f.ProductImages)
                        .Where(c => c.UserId == userId)
                        .ToListAsync();
        }


        public async Task DeleteItem(string userId, int productId, int SelectedSize)
        {
            //convert the size index to the size enum
            Size size;
            switch (SelectedSize)
            {
                case 0:
                    size = Size.Small;
                    break;
                case 1:
                    size = Size.Medium;
                    break;
                case 2:
                    size = Size.Large;
                    break;
                case 3:
                    size = Size.XLarge;
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



