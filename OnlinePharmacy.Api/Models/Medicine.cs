using System;
using System.Text.Json.Serialization;

namespace OnlinePharmacy.Api.Models
{
    public class Medicine
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool RequiresPrescription { get; set; }

        // Новое поле: ID категории
        public long CategoryId { get; set; }

        // Навигационное свойство (для связи)
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}