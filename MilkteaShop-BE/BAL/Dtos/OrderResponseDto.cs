using DAL.Models;

namespace BAL.Dtos
{
    public class OrderResponseDto
    {
        public string? OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public ICollection<OrderItemResponseDto> OrderItems { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public UserDto User { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
