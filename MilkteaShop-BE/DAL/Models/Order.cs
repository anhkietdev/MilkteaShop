namespace DAL.Models
{
    public class Order : BaseEntity
    {
        public required string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Guid PaymentMethodId { get; set; }
        public required virtual PaymentMethod PaymentMethod { get; set; }
        public Guid UserId { get; set; }
        public required virtual User User { get; set; }
    }
}
