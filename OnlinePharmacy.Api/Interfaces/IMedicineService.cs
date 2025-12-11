using System.Collections.Generic; // Для IEnumerable
using System.Threading.Tasks;    // Для Task
using OnlinePharmacy.Api.DTOs;   // Щоб бачити DTO
using OnlinePharmacy.Api.Models; // Щоб бачити Model

namespace OnlinePharmacy.Api.Interfaces
{
    public interface IMedicineService
    {
        Task<IEnumerable<Medicine>> GetAllAsync();
        Task<Medicine?> GetByIdAsync(long id); // ? означає, що може повернути null
        Task<Medicine> CreateAsync(MedicineDto dto);
        Task<bool> UpdateAsync(long id, MedicineDto dto);
        Task<bool> DeleteAsync(long id);
    }
}