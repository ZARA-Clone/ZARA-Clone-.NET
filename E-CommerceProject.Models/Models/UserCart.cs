using E_CommerceProject.Models.Enums;
using E_CommerceProject.Models.Models;

namespace E_CommerceProject.Models
{
    public class UserCart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Quantity { get; set; }

        public SizeEnum SelectedSize { get; set; }

        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
