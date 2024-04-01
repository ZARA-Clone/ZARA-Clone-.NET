using E_CommerceProject.Models.Models;

namespace E_CommerceProject.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Product> Products { get; set; } = new HashSet<Product>();

    }
}
