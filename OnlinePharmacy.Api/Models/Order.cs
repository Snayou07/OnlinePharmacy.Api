using System;
using System.Collections.Generic;

namespace OnlinePharmacy.Api.Models
{
    public class Order
    {
        public long Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; } // Общая сумма чека
        public string CustomerName { get; set; } = string.Empty; // ФИО покупателя

        // Связь: В заказе много позиций
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
