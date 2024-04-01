using E_CommerceProject.Models.Models;

namespace E_CommerceProject.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public IEnumerable<Order> Orders { get; set; } = new HashSet<Order>();
    }
}
