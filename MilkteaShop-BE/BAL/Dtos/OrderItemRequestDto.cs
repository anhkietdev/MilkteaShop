using DAL.Models;

namespace BAL.Dtos
{
    public class OrderItemRequestDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductSizeId { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
        public Guid? ParentOrderItemId { get; set; }
        public virtual ICollection<OrderItem>? ToppingItems { get; set; } 
    }
}
