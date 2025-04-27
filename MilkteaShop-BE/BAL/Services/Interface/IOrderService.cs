using BAL.Dtos;
using DAL.Models;

namespace BAL.Services.Interface
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(OrderRequestDto order);
        Task<ICollection<Order>> GetAllAsync();
        Task<Order> GetOrderByIdAsync(Guid id);
    }
}