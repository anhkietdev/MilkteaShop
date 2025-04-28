using DAL.Models;

namespace BAL.Dtos
{
    public class OrderRequestDto
    {
        public string? OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Guid UserId { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}
