using System.Text.Json.Serialization;

namespace OnlinePharmacy.Api.Models
{
    public class OrderItem
    {
        public long Id { get; set; }

        // Связь с заказом
        public long OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }

        // Связь с лекарством
        public long MedicineId { get; set; }
        public Medicine? Medicine { get; set; }

        public int Quantity { get; set; } // Сколько штук купили
        public decimal UnitPrice { get; set; } // Цена за штуку на момент покупки
    }
}