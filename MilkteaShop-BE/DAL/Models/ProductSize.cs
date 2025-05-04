namespace DAL.Models
{
    public class ProductSize : BaseEntity
    {
        public Guid ProductId { get; set; }
        public required virtual Product Product { get; set; }
        public Size? Size { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public virtual ICollection<ComboItem>? ComboItems { get; set; }
        public virtual ICollection<OrderItemTopping>? UsedAsToppingIn { get; set; }
    }
}
