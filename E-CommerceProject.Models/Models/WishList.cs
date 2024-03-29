using E_CommerceProject.Models.Models;

namespace E_CommerceProject.Models
{
    public class WishList
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public ApplicationUser User { get; set; }
        public Product Product { get; set; }
    }
}
