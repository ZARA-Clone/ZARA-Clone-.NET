namespace E_CommerceProject.Models
{
    public class Review
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; } 
        public Product Product { get; set; }


        public User User { get; set; }
    }
}
