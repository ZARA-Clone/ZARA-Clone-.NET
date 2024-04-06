using E_CommerceProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_CommerceProject.Infrastructure.Repositories.AddtoWishlist
{
    public  interface IaddToWishlist
    {

        Task<WishList> AddToWishlist(WishList wishList);
        
      Task removefromwishlist(WishList wishList);

        bool IsWishlist(int itemId, string userId);
    }
}
