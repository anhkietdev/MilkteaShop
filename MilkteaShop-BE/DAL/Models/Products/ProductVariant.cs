using DAL.Models.Orders;

namespace DAL.Models.Products
{
    public class ProductVariant : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid SizeId { get; set; }
        public decimal BasePrice { get; set; }
        public int StockQuantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<ProductVariantTopping> AvailableToppings { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
