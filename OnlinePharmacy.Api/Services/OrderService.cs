using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlinePharmacy.Api.Data;
using OnlinePharmacy.Api.DTOs;
using OnlinePharmacy.Api.Interfaces;
using OnlinePharmacy.Api.Models;

namespace OnlinePharmacy.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly PharmacyDbContext _context;
        public OrderService(PharmacyDbContext context) { _context = context; }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
           
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Medicine)
                .ToListAsync();
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                CustomerName = dto.CustomerName,
                OrderDate = DateTime.Now,
                Items = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            foreach (var itemDto in dto.Items)
            {
                
                var medicine = await _context.Medicines.FindAsync(itemDto.MedicineId);

                if (medicine == null)
                    throw new ArgumentException($"Ліки с ID {itemDto.MedicineId} не знайденно.");

              
                if (medicine.QuantityInStock < itemDto.Quantity)
                    throw new ArgumentException($"Невистачає товару '{medicine.Name}'. В наявності: {medicine.QuantityInStock}");

              
                medicine.QuantityInStock -= itemDto.Quantity;

       
                var orderItem = new OrderItem
                {
                    Medicine = medicine,
                    Quantity = itemDto.Quantity,
                    UnitPrice = medicine.Price
                };

                order.Items.Add(orderItem);
                totalAmount += medicine.Price * itemDto.Quantity;
            }

            order.TotalAmount = totalAmount;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
