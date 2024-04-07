namespace E_CommerceProject.Business.Dashborad.Orders.Dtos
{
    public class OrderProductsDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public decimal Discount { get; set; }
    }
}
