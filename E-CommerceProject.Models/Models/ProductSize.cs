using E_CommerceProject.Models.Enums;

namespace E_CommerceProject.Models
{
    public class ProductSize
    {
        public int Id {  get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public Size Size { get; set; }
        public Product Product { get; set; }
    }
}
