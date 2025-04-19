namespace DAL.Models
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public required virtual Order Order { get; set; }
        public Guid ProductId { get; set; }
        public required virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
