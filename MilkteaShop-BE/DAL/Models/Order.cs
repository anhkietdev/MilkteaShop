namespace DAL.Models
{
    public class Order : BaseEntity
    {
        public required decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public required IEnumerable<OrderDetail> OrderDetails { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
