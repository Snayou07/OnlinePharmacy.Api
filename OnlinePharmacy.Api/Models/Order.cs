using System;
using System.Collections.Generic;

namespace OnlinePharmacy.Api.Models
{
    public class Order
    {
        public long Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; } 
        public string CustomerName { get; set; } = string.Empty; 

        
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
