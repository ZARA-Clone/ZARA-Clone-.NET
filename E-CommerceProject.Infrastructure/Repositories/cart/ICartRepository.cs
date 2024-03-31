using E_CommerceProject.Models;
using E_CommerceProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_CommerceProject.Infrastructure.Repositories.cart
{
    public interface ICartRepository
    {
        Task<List<UserCart>> GetCartItemsAsync(string userId);
        // Task<bool> UpdateQuantityAsync(string userId, int productId, SizeEnum size, int quantity);
        //  Task<bool> DeleteCartItemAsync(string userId, int productId);

      
        int CheckAvailability(int productId, int size);
        bool updateProductQuantity(string userId,int productId,int quantity,int  sizeIndex);

        Task DeleteItem(string userId, int productId, int SelectedSize);

        Task AddItemToCart(UserCart userCart);

    }
}
