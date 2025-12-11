using System;
using System.ComponentModel.DataAnnotations;

namespace OnlinePharmacy.Api.DTOs
{
    public class MedicineDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Manufacturer { get; set; } = string.Empty;
        [Range(0.01, 100000)]
        public decimal Price { get; set; }
        [Range(0, 10000)]
        public int QuantityInStock { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool RequiresPrescription { get; set; }

        [Required]
        public long CategoryId { get; set; } 
    }
}
