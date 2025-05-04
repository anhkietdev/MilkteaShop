using BAL.Dtos;
using DAL.Models;

namespace BAL.Services.Interface
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(OrderRequestDto order);
        Task<ICollection<OrderResponseDto>> GetAllAsync();
        Task<OrderResponseDto> GetOrderByIdAsync(Guid id);
        Task<OrderResponseDto> UpdateOrderAsync(Guid id, OrderRequestDto order);
        Task<bool> DeleteOrderAsync(Guid id);
    }
}