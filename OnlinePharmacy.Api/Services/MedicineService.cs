using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Для ToListAsync, FindAsync
using OnlinePharmacy.Api.Data;
using OnlinePharmacy.Api.DTOs;
using OnlinePharmacy.Api.Interfaces;
using OnlinePharmacy.Api.Models;

namespace OnlinePharmacy.Api.Services
{
    // Реалізуємо інтерфейс IMedicineService
    public class MedicineService : IMedicineService
    {
        private readonly PharmacyDbContext _context;

        public MedicineService(PharmacyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Medicine>> GetAllAsync()
        {
            return await _context.Medicines.ToListAsync();
        }

        public async Task<Medicine?> GetByIdAsync(long id)
        {
            return await _context.Medicines.FindAsync(id);
        }

        public async Task<Medicine> CreateAsync(MedicineDto dto)
        {
            
            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (category == null)
            {
                throw new ArgumentException("Вказана категорія не існує!");
            }

         
            if (dto.ExpirationDate < DateTime.Now) throw new ArgumentException("Медикамент просрочено!");

            var medicine = new Medicine
            {
                Name = dto.Name,
                Manufacturer = dto.Manufacturer,
                Price = dto.Price,
                QuantityInStock = dto.QuantityInStock,
                ExpirationDate = dto.ExpirationDate,
                RequiresPrescription = dto.RequiresPrescription,
                CategoryId = dto.CategoryId 
            };

            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
            return medicine;
        }

        public async Task<bool> UpdateAsync(long id, MedicineDto dto)
        {
            var existing = await _context.Medicines.FindAsync(id);
            if (existing == null) return false;

            // Оновлюємо поля
            existing.Name = dto.Name;
            existing.Manufacturer = dto.Manufacturer;
            existing.Price = dto.Price;
            existing.QuantityInStock = dto.QuantityInStock;
            existing.ExpirationDate = dto.ExpirationDate;
            existing.RequiresPrescription = dto.RequiresPrescription;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null) return false;

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
