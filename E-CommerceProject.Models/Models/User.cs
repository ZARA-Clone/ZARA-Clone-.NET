using E_CommerceProject.Models.Enums;

namespace E_CommerceProject.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Country {  get; set; } = string.Empty;
        public string ZipCode {  get; set; } = string.Empty;
        public Role Role { get; set; }
        public UserCart Cart { get; set; }
        public IEnumerable<Order> Orders { get; set; } = new HashSet<Order>();
        public IEnumerable<Review> Reviews { get; set; } = new HashSet<Review>();
        public WishList WishList { get; set; }
    }
}
