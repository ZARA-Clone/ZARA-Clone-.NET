using E_CommerceProject.Models.Enums;

namespace E_CommerceProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country {  get; set; }
        public string ZipCode {  get; set; }
        public string Password {  get; set; }
        public Role Role { get; set; }
        public List<UserCart>  Carts { get; set; }
        public IEnumerable<Order> Orders { get; set; } = new HashSet<Order>();
        public IEnumerable<Review> Reviews { get; set; } = new HashSet<Review>();
        public WishList WishList { get; set; }
    }
}
