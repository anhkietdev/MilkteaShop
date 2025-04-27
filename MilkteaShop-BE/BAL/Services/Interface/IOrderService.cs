using DAL.Models;

namespace BAL.Services.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<ICollection<Order>> GetAllAsync();

        Task<Order> GetOrderByIdAsync(Guid id);
    }
}