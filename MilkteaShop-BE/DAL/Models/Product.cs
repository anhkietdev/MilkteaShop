namespace DAL.Models
{
    public class Product : BaseEntity
    {
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public required virtual Category Category { get; set; }
        public decimal Price { get; set; }   
        public string? ImageUrl { get; set; }
        public Size? Size { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public virtual ICollection<ComboItem>? ComboItems { get; set; }
    }
}
