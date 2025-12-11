using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlinePharmacy.Api.DTOs;
using OnlinePharmacy.Api.Interfaces;

namespace OnlinePharmacy.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrdersController(IOrderService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            try
            {
                var order = await _service.CreateOrderAsync(dto);
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}