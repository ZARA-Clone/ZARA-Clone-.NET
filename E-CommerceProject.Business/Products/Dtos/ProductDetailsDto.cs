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
        public List<Sizeenum> Sizes { get; set; }

    }
}
