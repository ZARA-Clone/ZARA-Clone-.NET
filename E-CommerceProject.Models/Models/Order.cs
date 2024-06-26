﻿using E_CommerceProject.Models.Models;

namespace E_CommerceProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        //public OrderStatus OrderStatus { get; set; }
        public ApplicationUser User { get; set; }
        public IEnumerable<OrderDetails> OrdersDetails { get; set; } = new HashSet<OrderDetails>();
    }
}
