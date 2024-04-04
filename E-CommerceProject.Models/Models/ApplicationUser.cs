using Microsoft.AspNetCore.Identity;

namespace E_CommerceProject.Models.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Address {  get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public List<UserCart> Carts { get; set; }
        public IEnumerable<Order> Orders { get; set; } = new HashSet<Order>();
        public IEnumerable<Review> Reviews { get; set; } = new HashSet<Review>();
        public WishList WishList { get; set; }


    }
}
