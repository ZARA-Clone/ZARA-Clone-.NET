using E_CommerceProject.Models.Enums;
using System.Drawing;

namespace E_CommerceProject.Business.Products.Dtos
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal? Discount { get; set; }
        public int Quantity { get; set; }
        public List<SizeEnum> Sizes { get; set; }
        public List<string> Images { get; set; }

    }
}
