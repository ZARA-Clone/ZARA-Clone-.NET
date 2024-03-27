namespace E_CommerceProject.Models
{
    public class UserCart
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public Product  Product { get; set; }
        public User User { get; set; }
    }
}
