using BAL.Dtos;
using DAL.Models;

namespace BAL.Services.Interface
{
    public interface IOrderItemService
    {
        Task<ICollection<OrderItem>> GetAllOrderItemsAsync();
        Task<OrderItem> GetOrderItemByIdAsync(Guid id);
        Task CreateOrderItemAsync(OrderItemRequestDto orderItemDto);
        Task UpdateOrderItemAsync(Guid id, OrderItemRequestDto orderItemDto);
        Task<bool> DeleteOrderItemAsync(Guid id);
    }
}
