namespace E_CommerceProject.Business.Users.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<UserDashboardOrderDto> Orders { get; set; }
    }

    public class UserDashboardOrderDto
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? DeliverdDate { get; set; }

    }
}
