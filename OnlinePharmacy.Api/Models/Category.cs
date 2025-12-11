using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OnlinePharmacy.Api.Models
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Связь: В категории много лекарств. JsonIgnore чтобы не было циклов при выводе.
        [JsonIgnore]
        public List<Medicine> Medicines { get; set; } = new List<Medicine>();
    }
}