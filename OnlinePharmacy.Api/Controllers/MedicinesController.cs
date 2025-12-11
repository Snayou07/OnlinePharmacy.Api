using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; // Для ControllerBase, HttpGet та ін.
using OnlinePharmacy.Api.DTOs;
using OnlinePharmacy.Api.Interfaces;

namespace OnlinePharmacy.Api.Controllers
{
    [ApiController] // Вказує, що це API, а не сайт
    [Route("api/[controller]")] // Шлях буде /api/medicines
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineService _service;

        // Впроваджуємо саме Інтерфейс! Це дозволить потім робити тести.
        public MedicinesController(IMedicineService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result); // 200 OK
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var medicine = await _service.GetByIdAsync(id);
            if (medicine == null) return NotFound(); // 404 Not Found
            return Ok(medicine);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MedicineDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // Перевірка валідації (ціна > 0 і т.д.)

            try
            {
                var created = await _service.CreateAsync(dto);
                // Повертає 201 Created і посилання на створений об'єкт
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex) // Ловимо наші помилки бізнес-логіки
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] MedicineDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent(); // 204 No Content (успіх, але без тіла відповіді)
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}