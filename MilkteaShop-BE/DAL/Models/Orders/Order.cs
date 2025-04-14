namespace DAL.Models.Orders
{
    public class Order : BaseEntity
    {
        public required decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public required IEnumerable<OrderItem> OrderItem { get; set; }
        public Guid UserId { get; set; }
    }
}
