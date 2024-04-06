using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Models;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceProject.Infrastructure.Repositories.AddtoWishlist
{



    public class AddtoWishlist : IaddToWishlist
    {
        private readonly ECommerceContext _context;

        public AddtoWishlist(ECommerceContext context)
        {
            _context = context;
        }



        public async Task<WishList> AddToWishlist(WishList wishListitem)
        {

            _context.WishLists.Add(wishListitem);
           await  _context.SaveChangesAsync();
            return wishListitem;


        }

       

        public async Task removefromwishlist(WishList wishlistitem)
        {

            var product=_context.WishLists.FirstOrDefault(u=>u.ProductId== wishlistitem.ProductId && u.UserId==wishlistitem.UserId);


            if(product!=null)
            {
                _context.WishLists.Remove(product);
                await _context.SaveChangesAsync();
            }
        }



        public bool IsWishlist(int itemId, string userId)
        {
            var wishlist = _context.WishLists.FirstOrDefault(u => u.UserId == userId && u.ProductId == itemId);
            return wishlist != null;
        }




    }
}
