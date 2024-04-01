using E_CommerceProject.Models.Enums;

namespace E_CommerceProject.Business.Cart.Dtos
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
        public int Quantity { get; set; }
        public Size Size { get; set; }

    }
}





