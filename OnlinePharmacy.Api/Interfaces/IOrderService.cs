using System.Collections.Generic;
using System.Threading.Tasks;
using OnlinePharmacy.Api.DTOs;
using OnlinePharmacy.Api.Models;

namespace OnlinePharmacy.Api.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> CreateOrderAsync(CreateOrderDto dto);
    }
}