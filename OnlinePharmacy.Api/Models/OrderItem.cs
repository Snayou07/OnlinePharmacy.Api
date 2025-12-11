using System.Text.Json.Serialization;

namespace OnlinePharmacy.Api.Models
{
    public class OrderItem
    {
        public long Id { get; set; }

     
        public long OrderId { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }

        
        public long MedicineId { get; set; }
        public Medicine? Medicine { get; set; }

        public int Quantity { get; set; } 
        public decimal UnitPrice { get; set; } 
    }
}
