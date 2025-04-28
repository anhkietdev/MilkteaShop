using DAL.Models;

namespace BAL.Dtos
{
    namespace BLL.Dtos
    {
        public class OrderDto
        {
            public Guid Id { get; set; }
            public string OrderNumber { get; set; } = default!;
            public decimal TotalAmount { get; set; }
            public string? Description { get; set; }
            public OrderStatus Status { get; set; }
            public Guid PaymentMethodId { get; set; }
            public string PaymentMethodName { get; set; } = default!;
            public Guid UserId { get; set; }
            public string UserName { get; set; } = default!;
        }
    }
}