namespace E_CommerceProject.Business.Dashborad.Orders.Dtos
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public int ProductCount { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderProductsDto> OrderProducts { get; set; }
    }
}
