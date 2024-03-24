namespace E_CommerceProject.Business.Products.Dtos
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal NetPrice => Math.Round(Price - (Price * Discount / 100), 0);
        public decimal Rating { get;set; }
        public decimal AvgRating => Math.Round(Rating, 1);
        public int ReviewsCount { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
