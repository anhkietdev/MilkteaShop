namespace DAL.Models
{
    public class ProductSize : BaseEntity
    {
        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public Size? Size { get; set; }
        public decimal Price { get; set; }
    }
}
