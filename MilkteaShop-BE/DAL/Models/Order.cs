namespace DAL.Models
{
    public class Order : BaseEntity
    {
        public required string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public PaymentMethod PaymentMethod { get; set; }
        public Guid UserId { get; set; }
        public required virtual User User { get; set; }
        public static string GenerateOrderNumber()
        {
            var random = new Random();
            var randomNumber = random.Next(100000, 999999);
            var orderPrefix = "ORD";
            var orderNumber = $"{orderPrefix}-{DateTime.Now:yyyyMMdd}-{randomNumber}";
            return orderNumber;
        }
    }
}
