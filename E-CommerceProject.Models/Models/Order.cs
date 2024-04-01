using E_CommerceProject.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_CommerceProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public User User { get; set; }
        public IEnumerable<OrderDetails> OrdersDetails { get; set; } = new HashSet<OrderDetails>();
    }
}
