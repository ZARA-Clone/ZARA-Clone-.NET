namespace E_CommerceProject.Business.Dashborad.Orders.Dtos
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int ProductCount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
