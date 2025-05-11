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
        Task<OrderSummaryDto> GetTodayOrdersAsync(Guid? storeId = null);
        Task<OrderSummaryDto> GetWeekOrdersAsync(Guid? storeId = null);
        Task<OrderSummaryDto> GetMonthOrdersAsync(Guid? storeId = null);
        Task<OrderSummaryDto> GetYearOrdersAsync(Guid? storeId = null);
        Task<ICollection<OrderSummaryDto>> GetOrdersByStoreAsync(Guid storeId);
        Task<bool> ApplyVoucher(ApplyVoucherDto applyVoucherDto);
        Task<ICollection<TopSellingProductDto>> GetTop5BestSellingProductsAsync();
        Task<OrderResponseDto> CreateOrderComboAsync(OrderComboRequest orderRequest);

        Task<RequiredOrderDto> GetOrderRelatedAmount(Guid orderId);
    }
}