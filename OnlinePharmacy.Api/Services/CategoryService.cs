using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlinePharmacy.Api.Data;
using OnlinePharmacy.Api.DTOs;
using OnlinePharmacy.Api.Interfaces;
using OnlinePharmacy.Api.Models;

namespace OnlinePharmacy.Api.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly PharmacyDbContext _context;
        public CategoryService(PharmacyDbContext context) { _context = context; }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.ToListAsync();

        public async Task<Category> CreateAsync(CategoryDto dto)
        {
            var category = new Category { Name = dto.Name };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}