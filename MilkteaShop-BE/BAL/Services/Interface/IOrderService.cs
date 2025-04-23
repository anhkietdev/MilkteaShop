using DAL.Models;

namespace BAL.Services.Interface
{
    public interface IOrderService
    {
     Task<ICollection<Order>> GetAllAsync();
        Task<Order> GetOrderByIdAsync(Guid id);
    }
}