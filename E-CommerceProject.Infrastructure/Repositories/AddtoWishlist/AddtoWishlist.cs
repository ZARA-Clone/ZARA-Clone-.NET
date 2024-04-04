using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Models;
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
    }
}
