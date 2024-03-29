using E_CommerceProject.Models.Models;

namespace E_CommerceProject.Models
{
    public class Review
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public DateTime Created { get; set; } 
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
