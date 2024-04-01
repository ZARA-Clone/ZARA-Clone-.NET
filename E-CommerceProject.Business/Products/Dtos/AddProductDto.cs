namespace E_CommerceProject.Business.Products.Dtos
{
    public class AddProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int BrandId { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public List<KeyValuePair<string, int>> Sizes { get; set; } = new List<KeyValuePair<string, int>>();
    }
}
