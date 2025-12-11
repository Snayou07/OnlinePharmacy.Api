using System.ComponentModel.DataAnnotations;

namespace OnlinePharmacy.Api.DTOs
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}