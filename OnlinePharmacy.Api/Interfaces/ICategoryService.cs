using System.Collections.Generic;
using System.Threading.Tasks;
using OnlinePharmacy.Api.DTOs;
using OnlinePharmacy.Api.Models;

namespace OnlinePharmacy.Api.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> CreateAsync(CategoryDto dto);
    }
}