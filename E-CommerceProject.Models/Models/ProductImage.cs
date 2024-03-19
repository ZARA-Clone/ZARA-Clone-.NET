namespace E_CommerceProject.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public Product Product { get; set; }
    }
}
