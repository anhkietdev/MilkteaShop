using DAL.Models;

namespace BAL.Dtos
{
    public class OrderResponseDto
    {
        public Guid Id { get; set; }
        public required string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public ICollection<OrderItemResponseDto>? OrderItems { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid UserId { get; set; }
        public Guid? StoreId { get; set; }
        public required virtual Store Store { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
