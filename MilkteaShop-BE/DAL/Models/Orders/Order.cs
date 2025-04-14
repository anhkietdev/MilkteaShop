namespace DAL.Models.Orders
{
    public class Order : BaseEntity
    {
        public required string OrderNumber { get; set; }
        public Guid UserId { get; set; } 
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }

        public string? Note { get; set; }
        public DateTime? CompletedAt { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
