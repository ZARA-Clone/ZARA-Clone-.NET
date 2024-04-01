namespace E_CommerceProject.Models
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
