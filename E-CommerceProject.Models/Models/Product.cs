namespace E_CommerceProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } =string.Empty;
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string Description { get; set; } = string.Empty;
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public List<UserCart> UserCarts { get; set; } = new List<UserCart>();
        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<WishList> WishLists { get; set; } = new List<WishList>();
        public List<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();



    }
}
