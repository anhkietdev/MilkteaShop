namespace DAL.Models
{
    public class ProductSize : BaseEntity
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Size? Size { get; set; }
        public decimal Price { get; set; }
    }
}
