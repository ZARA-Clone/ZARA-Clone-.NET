using E_CommerceProject.Models.Enums;

namespace E_CommerceProject.Models.Models
{
    public class Size
    {
        public int Id {  get; set; }
        public Sizeenum Name { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
