using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlinePharmacy.Api.DTOs
{
    // Покупатель присылает список ID лекарств и количество
    public class CreateOrderItemDto
    {
        [Required]
        public long MedicineId { get; set; }
        [Range(1, 100)]
        public int Quantity { get; set; }
    }

    public class CreateOrderDto
    {
        [Required]
        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public List<CreateOrderItemDto> Items { get; set; } = new List<CreateOrderItemDto>();
    }
}