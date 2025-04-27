namespace DAL.Models
{
    public class Product : BaseEntity
    {
        public required string ProductName { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public required virtual Category Category { get; set; }
        public string? ImageUrl { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public virtual ICollection<ComboItem>? ComboItems { get; set; }
        public string? ProductType { get; set; }
        public virtual ICollection<ProductSize> ProductSizes { get; set; } = new List<ProductSize>();
    }
}

