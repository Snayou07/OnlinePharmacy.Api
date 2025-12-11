using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlinePharmacy.Api.DTOs;
using OnlinePharmacy.Api.Interfaces;

namespace OnlinePharmacy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoriesController(ICategoryService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            var category = await _service.CreateAsync(dto);
            return Ok(category);
        }
    }
}