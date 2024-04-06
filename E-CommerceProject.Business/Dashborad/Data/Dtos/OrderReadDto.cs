namespace E_CommerceProject.Business.Dashborad.Data.Dtos
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ProductCount { get; set; }
        public decimal TotalPrice { get; set; }
        //public string CreationDate => OrderDate.ToString("dd-MM-yyyy");
    }
}
