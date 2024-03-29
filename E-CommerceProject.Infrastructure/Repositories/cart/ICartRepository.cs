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
        Task<bool> UpdateQuantityAsync(string userId, int productId, SizeEnum size, int quantity);
        Task<bool> DeleteCartItemAsync(string userId, int productId);
    }
}
